using UnityEngine;
using UnityEngine.InputSystem;

public class InputHander : MonoBehaviour
{
    public static InputHander Instance { get; private set; }

    [Header("Input Actions")]
    public InputActionProperty Horizontal;
    public InputActionProperty Jump;
    public InputActionProperty WallHold;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnEnable()
    {
        EnableInputs();
    }

    private void OnDisable()
    {
        DisableInputs();
    }

    // ── Enable / Disable ──────────────────────────────────────────

    public void EnableInputs()
    {
        Horizontal.action?.Enable();
        Jump.action?.Enable();
        WallHold.action?.Enable();
    }

    public void DisableInputs()
    {
        Horizontal.action?.Disable();
        Jump.action?.Disable();
        WallHold.action?.Disable();
    }

    // ── Input Readers ─────────────────────────────────────────────

    public Vector2 GetHorizontalInput()
    {
        return Horizontal.action?.ReadValue<Vector2>() ?? Vector2.zero;
    }

    public bool JumpPressed()
    {
        return Jump.action?.WasPressedThisFrame() ?? false;
    }

    public bool WallHoldPressed()
    {
        return WallHold.action?.IsPressed() ?? false;
    }
}