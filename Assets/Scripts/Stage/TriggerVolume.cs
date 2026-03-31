using UnityEngine;
using UnityEngine.UIElements;

public class TriggerVolume : MonoBehaviour
{
    // If true, trigger field will only toggle once and will not disable it's target when it's empty
    [SerializeField]
    private bool oneTimeTrigger;

    [SerializeField]
    private bool triggeredByPlayer, triggeredByPhysicsObjects;

    [SerializeField]
    private GateController gateController;

    private int currentTriggers = 0;
    private bool isActive = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Touch");
        if (triggeredByPlayer && collision.CompareTag("Player"))
        {
            currentTriggers++;
        }

        if (triggeredByPhysicsObjects && collision.CompareTag("PhysicsObject"))
        {
            currentTriggers++;
        }
        CheckState();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!oneTimeTrigger)
        {
            if (triggeredByPlayer && collision.CompareTag("Player"))
            {
                currentTriggers--;
            }

            if (triggeredByPhysicsObjects && collision.CompareTag("PhysicsObject"))
            {
                currentTriggers--;
            }
            CheckState();
        }
    }

    private void CheckState()
    {
        if (!isActive && currentTriggers > 0)
        {
            isActive = true;
            gateController.UpdateState(isActive);
        }

        if (isActive && currentTriggers == 0)
        {
            isActive = false;
            gateController.UpdateState(isActive);
        }
    }
}
