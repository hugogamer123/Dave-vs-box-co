using System;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField]
    private int requiredActivations;
    private int currentActivations;

    private Collider2D gateCollider;
    private Renderer spriteRenderer;

    private void Awake()
    {
        gateCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<Renderer>();
    }

    public void UpdateState(bool isActive)
    {
        currentActivations += isActive ? 1 : -1;

        currentActivations = Mathf.Max(0, currentActivations);

        if (currentActivations >= requiredActivations)
        {
            OpenGate();
        }
        else
        {
            CloseGate();
        }
    }

    private void CloseGate()
    {
        gateCollider.enabled = true;
        spriteRenderer.enabled = true;
    }

    private void OpenGate()
    {
        gateCollider.enabled = false;
        spriteRenderer.enabled = false;
    }
}
