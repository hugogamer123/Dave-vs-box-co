using System.Collections;
using UnityEngine;

public class GorilaMovement : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb2d;
    public Collider2D coll2D;
    public Transform player;

    [SerializeField]
    private Transform groundCheck1;

    [SerializeField]
    private Transform groundCheck2;
    public LayerMask groundLayer;

    [Header("Move")]
    public float speed;
    public float waitTimeAtEdge;
    private bool canMove;

    [Header("Attack")]
    public float attackRange = 4f;
    public bool isPlayerDetected;

    public float playerHorizontalDistance = 12f;
    public float minPlayerVerticalDistance = 4f;
    public float maxPlayerVerticalDistance = 10f;

    [Header("Script References")]
    public Movement movement;

    private bool isGrounded;
    private bool isEdged;
    private bool isWaitAtEdge;

    private float originScaleX;

    public bool faceRight { get; private set; } = false;

    private void Start()
    {
        originScaleX = transform.localScale.x;
    }

    private void FixedUpdate()
    {
        GroundCheck();
        PlayerCheck();

        Attack();
        Move();
    }

    private void Move()
    {
        if (!canMove)
        {
            return;
        }
        float direction = faceRight ? 1 : -1;

        rb2d.linearVelocity = new Vector2(direction * speed, rb2d.linearVelocity.y);
    }

    private void StopMove()
    {
        rb2d.linearVelocity = new Vector2(0, rb2d.linearVelocity.y);
    }

    private void Attack()
    {
        if (isPlayerDetected)
        {
            canMove = true;
            var distance = Vector2.Distance(transform.position, player.position);
            if (distance < attackRange)
            {
                canMove = false;
                return;
            }
            // Attack
            Debug.Log($"{name} Attack {player.name}");
        }
    }

    private void GroundCheck()
    {
        if (isWaitAtEdge)
            return;
        var hit1 = Physics2D.Raycast(groundCheck1.position, Vector2.down, 0.1f, groundLayer);

        var hit2 = Physics2D.Raycast(groundCheck2.position, Vector2.down, 0.1f, groundLayer);

        isGrounded = hit1 || hit2;
        if (isPlayerDetected)
        {
            return;
        }

        canMove = isGrounded;

        isEdged = !hit1;

        if (isEdged && isGrounded)
        {
            canMove = false;
            isWaitAtEdge = true;
            StopMove();
            StartCoroutine(WaitAtEdge());
        }
    }

    private IEnumerator WaitAtEdge()
    {
        yield return new WaitForSeconds(waitTimeAtEdge);
        if (!isPlayerDetected)
        {
            faceRight = !faceRight;
            FlipX();
        }
        canMove = true;
        isWaitAtEdge = false;
        Debug.Log("Wait finished.");
    }

    private void PlayerCheck()
    {
        // Don't check if player position lower or higher then transform.
        var verticalDistance = transform.position.y - player.position.y;
        if (
            verticalDistance > minPlayerVerticalDistance
            || verticalDistance < -maxPlayerVerticalDistance
        )
        {
            isPlayerDetected = false;
            return;
        }
        var horizontalDistance = Mathf.Abs(transform.position.x - player.position.x);

        if (horizontalDistance < playerHorizontalDistance)
        {
            var playerDirection = Mathf.Sign(player.position.x - transform.position.x);
            faceRight = playerDirection > 0;
            FlipX();
            isPlayerDetected = true;
        }
        else
        {
            isPlayerDetected = false;
        }
    }

    private void FlipX()
    {
        var scale = faceRight ? -1 : 1;
        transform.localScale = new Vector3(
            scale * originScaleX,
            transform.localScale.y,
            transform.localScale.z
        );
    }
}
