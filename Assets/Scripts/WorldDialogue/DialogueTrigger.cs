using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] DialogueData dialogue;

    private bool playerInRange;

    void Update()
    {
        if (playerInRange && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return)))
            DialogueManager.Instance?.StartDialogue(dialogue);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
