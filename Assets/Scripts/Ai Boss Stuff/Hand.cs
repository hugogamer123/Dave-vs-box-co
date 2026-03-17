using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Hand : MonoBehaviour
{
    [SerializeField] Transform PlayerTransform;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Vector3 HandMoveTo;

    [SerializeField] LayerMask GroundLayer;

    [SerializeField] float HandDownForce;
    [SerializeField] float GoBackForce;

    [SerializeField] float Duration1;
    [SerializeField] float Duration2;
    [SerializeField] float Duration3;
    [SerializeField] float HandUpDuration;

    //Make sures both hands are dead
    [Header("Hand Death")]
    public float HandHealth;
    [SerializeField] bool HandIsDead;

    //List Of all Hand Colliders
    //MAKE SURE THIS ONLY CONTAINS DAMAGING COLLIDERS!! NOT THE MAIN COLLIDER OR ELSE HAND WILL FALL THROUGH FLOOR
    public List<Collider2D> Colliders;

    public bool IsHandLDead;
    public bool IsHandRDead;

    Vector3 initialPosition;

    public BossGeneral bossGeneral;
    public void Attack()
    {
        StartCoroutine(HandAttack(Duration1, Duration2, Duration3, HandUpDuration));
    }

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        HandMoveTo = new Vector3(PlayerTransform.position.x, -1f, 0);

        if(HandHealth == 0){HandIsDead = true;}

        if (HandIsDead)
        {
            StartHandDeath();
        }
    }

    IEnumerator HandAttack(float duration1, float duration2, float duration3, float handupduration)
    {
        float elapsed1 = 0f;

        //Follows Player
        while (elapsed1 < duration1)
        {

            rb.MovePosition(HandMoveTo);

            elapsed1 += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        //Moves hand up a lil to indicate its attack.

        float elapsed4 = 0f;
        while (elapsed4 < handupduration)
        {
            Vector3 targetPosition = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), HandDownForce * Time.fixedDeltaTime);
            rb.MovePosition(targetPosition);
            elapsed4 += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        float elapsed2 = 0f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, GroundLayer);
        float GroundDistance = hit.distance;
        Debug.Log("Ground Distance: " + GroundDistance);

        Vector3 FloorHit = new Vector3(transform.position.x, -GroundDistance + transform.position.y + 1f, 0);
        Debug.Log($"FloorHit Vector: {FloorHit}");

        //Strikes down
        while (elapsed2 < duration2)
        {
            Vector3 targetPosition = Vector3.MoveTowards(transform.position, FloorHit, HandDownForce * Time.fixedDeltaTime);
            rb.MovePosition(targetPosition);
            Debug.Log($"Current Position: {transform.position}, Target Position: {targetPosition}");
            //rb.MovePosition(FloorHit);
            elapsed2 += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        float elapsed3 = 0f;
        //Goes back to OG position
        while (elapsed3 < duration3)
        {
            Vector3 targetPosition = Vector3.MoveTowards(transform.position, initialPosition, GoBackForce * Time.fixedDeltaTime);

            rb.MovePosition(targetPosition);
            elapsed3 += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        Debug.Log("Hand Attack Finished");

        bossGeneral.Phase1Attacking = false;
    }

    void StartHandDeath()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;

        foreach (var collision in Colliders)
        {
            collision.enabled = false;
        }
    }
}
