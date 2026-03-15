using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform Player;

    [SerializeField] Vector3 PlayerPos;
    [SerializeField] Vector3 CameraPos;

    [SerializeField] Transform Bound1;
    [SerializeField] Transform Bound2;

    private void Update()
    {
        PlayerPos = Player.position;
    }

    private void LateUpdate()
    {
        //Clamping cam.
        float XDir = Mathf.Clamp(PlayerPos.x, Bound1.position.x, Bound2.position.x);

        transform.position = new Vector3(XDir, transform.position.y, -10);
    }
}