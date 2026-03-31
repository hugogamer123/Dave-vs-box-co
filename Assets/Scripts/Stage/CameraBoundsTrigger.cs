using UnityEngine;

public class CameraBoundsTrigger : MonoBehaviour
{
    [SerializeField]
    CameraFollow cameraFollow;

    [SerializeField]
    Transform Bound1, Bound2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cameraFollow.SetBound1(Bound1.transform);
            cameraFollow.SetBound2(Bound2.transform);
        }
    }
}
