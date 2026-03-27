using UnityEngine;

public class HandSwitch : MonoBehaviour
{

    [SerializeField] Hand hand;
    [SerializeField] BossGeneral bossGeneral;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(bossGeneral.whatAttackuse == "slam")
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                hand.HandHealth--;
                Debug.LogWarning($"Remaining Health On Hand:{hand.HandHealth}");
            }
        }
    }
}