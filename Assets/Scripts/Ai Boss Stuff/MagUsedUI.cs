using UnityEngine;
using UnityEngine.UI;

public class MagUsedUI : MonoBehaviour
{
    [SerializeField] MagnetController magnet;

    [Header("Different Magnet Sprites")]
    [SerializeField] Image UiImage;
    [Space]
    [SerializeField] Sprite PullSpr;
    [SerializeField] Sprite PushSpr;
    [SerializeField] Sprite LaserSpr;

    private void Update()
    {
        switch (magnet.activeMagnet)
        {
            case MagnetController.MagnetType.Pull:
                UiImage.sprite = PullSpr;
                break;
            case MagnetController.MagnetType.Push:
                UiImage.sprite = PushSpr;
                break;
            case MagnetController.MagnetType.Laser:
                UiImage.sprite = LaserSpr;
                break;
        }
    }
}
