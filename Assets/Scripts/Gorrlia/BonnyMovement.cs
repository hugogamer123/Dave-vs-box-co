using UnityEngine;

public class BonnyMovement : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb2d;
    public Transform player;

    [SerializeField]
    private Transform groundCheck1;

    [SerializeField]
    private Transform groundCheck2;
    public LayerMask groundLayer;

    [Header("Move")]
    public float jumpForce;
    public float jumpHorizontalForce;
    public float jumpCooldown;
    private float jumpTimer;

    public Transform target;

    [Header("Chasing Player")]
    public float chaseRange;

    private bool isPlayerDetected;
    private bool isGrounded;
    private bool isEdged;

    private float originScaleX;

    public bool faceRight { get; private set; } = false;

    private void Start()
    {
        originScaleX = transform.localScale.x;
    }

    private void FixedUpdate()
    {
        GroundCheck();
        //PlayerCheck();

        //Move();
        //if (isEdged && isGrounded && jumpTimer < jumpCooldown)
        //{
        //    faceRight = !faceRight;
        //    FlipX();
        //}
        JumpToTarget();
    }

    private void Move()
    {
        if (!isGrounded)
        {
            return;
        }
        if (jumpTimer < jumpCooldown)
        {
            jumpTimer += Time.deltaTime;
            return;
        }

        float direction = faceRight ? 1 : -1;

        rb2d.AddForce(new Vector2(jumpHorizontalForce * direction, jumpForce), ForceMode2D.Impulse);
        jumpTimer = 0;
    }

    private void JumpToTarget()
    {
        if (!isGrounded)
        {
            return;
        }
        if (jumpTimer < jumpCooldown)
        {

            jumpTimer += Time.deltaTime;
            return;
        }
        float dx = target.position.x - transform.position.x;
        float distance = Mathf.Abs(dx);

        float direction = Mathf.Sign(dx);

        float horizontalForce = jumpHorizontalForce;
        var gravity = Physics2D.gravity.y * rb2d.gravityScale * rb2d.mass;

        float jumpTime = 2 * jumpForce / -gravity;
        float maxJumpDistance = jumpTime * jumpHorizontalForce;
        ;

        if (distance <= maxJumpDistance)
        {
            horizontalForce = jumpHorizontalForce * (distance / maxJumpDistance);
        }
        rb2d.linearVelocity = Vector2.zero;
        rb2d.AddForce(new Vector2(horizontalForce * direction, jumpForce), ForceMode2D.Impulse);
        jumpTimer = 0;
    }

    private void GroundCheck()
    {
        var hit1 = Physics2D.Raycast(groundCheck1.position, Vector2.down, 0.2f, groundLayer);
        var hit2 = Physics2D.Raycast(groundCheck2.position, Vector2.down, 0.2f, groundLayer);
        isGrounded = hit1 || hit2;
        isEdged = !hit1;

    }

    private void PlayerCheck()
    {
        var distance = Vector2.Distance(transform.position, player.position);
        if (distance < chaseRange)
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
