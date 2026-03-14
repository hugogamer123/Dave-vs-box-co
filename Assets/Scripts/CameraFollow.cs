using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 targetPoint = Vector3.zero;

    public Movement player;

    public float moveSpeed = 5f;

    public float lookAheadDistance = 5f, lookAheadSpeed = 3f;

    private float lookOffset;

    private bool isFalling;
    public float maxVertOffset = 5f;

    public Transform leftBoundary;
    public Transform rightBoundary;

    bool IsClamped = false;
    public LayerMask clampMask;
    private void Start()
    {
        targetPoint = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == clampMask) { IsClamped = true; }
        else { IsClamped = false; }
    }
    private void LateUpdate()
    {
        //targetPoint.x = player.transform.position.x;
        //targetPoint.y = player.transform.position.y;

        //Trying to restrict camera.
        float clampedX = Mathf.Clamp(transform.position.x, leftBoundary.position.x, rightBoundary.position.x);

        if (player.isGrounded)
        {
            targetPoint.y = player.transform.position.y;
        }

        if(transform.position.y - player.transform.position.y > maxVertOffset)
        {
            isFalling = true;
        }
        else if(player.transform.position.y - transform.position.y > maxVertOffset)
        {
            isFalling = false;
        }
         
        if(isFalling)
        {
            targetPoint.y = player.transform.position.y;

            if(player.isGrounded)
            {
                isFalling = false;
            }
        }

        if (targetPoint.y < 0)
        {
            targetPoint.y = 0;
        }

        if (!IsClamped)
        {
            if (player.moveInput.x > 0f)
            {
                lookOffset = Mathf.Lerp(lookOffset, lookAheadDistance, lookAheadSpeed * Time.deltaTime);
            }
            else if (player.moveInput.x < 0f)
            {
                lookOffset = Mathf.Lerp(lookOffset, -lookAheadDistance, lookAheadSpeed * Time.deltaTime);
            }
        }

        targetPoint.x = player.transform.position.x + lookOffset;

        Vector3 finalPos = new Vector3(clampedX, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(finalPos, targetPoint, moveSpeed * Time.deltaTime);
    }
}