using UnityEngine;

public class NextAreaTrigger : MonoBehaviour
{
    [SerializeField]
    Transform teleportLocation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.position = teleportLocation.position;
        }
    }
}
