using UnityEngine;

[RequireComponent(typeof(MagnetController))]
public class SelectMagToUse : MonoBehaviour
{
    [SerializeField] private InputHander inputHander;
    private MagnetController magnet;
    void Start()
    {
        magnet = GetComponent<MagnetController>();
    }

    void Update()
    {
        if(inputHander.Mag1Pressed())
        {
            magnet.SelectedMagnet = MagnetController.MagnetType.Pull;
            magnet.StopMagnets();

            Debug.Log("Pull Magnet activated");
        }
        else if(inputHander.Mag2Pressed())
        {
            magnet.SelectedMagnet = MagnetController.MagnetType.Push;
            magnet.StopMagnets();

            Debug.Log("Push Magnet activated");
        }
        else if(inputHander.Mag3Pressed())
        {
            magnet.SelectedMagnet = MagnetController.MagnetType.Laser;
            magnet.StopMagnets();

            Debug.Log("Lazer Magnet activated");
        }
    }
}
