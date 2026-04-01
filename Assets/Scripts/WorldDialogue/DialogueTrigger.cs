using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] DialogueData dialogue;

    public void OnInteract(Component sender, object parameter)
    {
        if (sender.CompareTag("Player"))
            DialogueManager.Instance?.StartDialogue(dialogue);
    }
}
