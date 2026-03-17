using UnityEngine;

public class HandSwitch : MonoBehaviour
{

    [SerializeField] Hand hand;
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hand.HandHealth--;
        }
    }
}
