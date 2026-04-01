using System.Collections;
using UnityEngine;

public class LouiseNPC : MonoBehaviour
{
    [SerializeField] HeartUI heartUI;
    [SerializeField] LouiseHealPrompt healPrompt;
    [SerializeField] DialogueData preHealDialogue;
    [SerializeField] DialogueData acceptDialogue;
    [SerializeField] DialogueData denyDialogue;

    private bool playerInRange;
    private bool sequenceRunning;

    public void OnInteract(Component sender, object parameter)
    {
        if (sender.CompareTag("Player"))
            StartCoroutine(HealSequence());
    }

    IEnumerator HealSequence()
    {
        sequenceRunning = true;

        DialogueManager.Instance.StartDialogue(preHealDialogue);
        yield return new WaitUntil(() => !DialogueManager.Instance.IsActive);

        bool? choice = null;
        healPrompt.Show(() => choice = true, () => choice = false);
        yield return new WaitUntil(() => choice.HasValue);

        if (choice.Value)
        {
            heartUI.FullHeal();
            DialogueManager.Instance.StartDialogue(acceptDialogue);
            yield return new WaitUntil(() => !DialogueManager.Instance.IsActive);
        }
        else
        {
            DialogueManager.Instance.StartDialogue(denyDialogue);
            yield return new WaitUntil(() => !DialogueManager.Instance.IsActive);
        }

        sequenceRunning = false;
    }
}
