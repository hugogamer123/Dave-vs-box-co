using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHander))]
public class InteractionCaller : MonoBehaviour
{
    [SerializeField] private float interactionRange = 1f;
    [SerializeField] private LayerMask interactionLayerMask;
    [SerializeField] private GameEvent onInteract;
    public bool canInteract = true;

    // Update is called once per frame
    void Update()
    {
        if (!(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return)))
            return;

        if (!canInteract)
            return;


        var cast = Physics2D.Raycast((Vector2)transform.position, Vector2.right * transform.localScale.x, interactionRange, interactionLayerMask);

        if (cast)
        {
            onInteract.Raise(this, cast.collider.gameObject, null);
            Debug.Log("Interaction");
        }
    }

    public void EnableInteraction() => canInteract = true;
    public void DisableInteraction() => canInteract = false;
    public void CooldownInteraction()
    {
        StartCoroutine(CooldownCoroutine(.5f));
    }

    private IEnumerator CooldownCoroutine(float duration)
    {
        DisableInteraction();
        yield return new WaitForSeconds(duration);
        EnableInteraction();
    }
}
