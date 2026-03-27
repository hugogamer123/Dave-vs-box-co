using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHander : MonoBehaviour
{
    public static InputHander Instance { get; private set; }

    [Header("Input Actions")]
    public InputActionProperty Horizontal;
    public InputActionProperty Jump;
    public InputActionProperty WallHold;
    public InputActionProperty PullObj;
    public InputActionProperty PushObj;
    public InputActionProperty QuickDash;
    public InputActionProperty MagnetBut;

    [Header("Different Magnet Functions")]
    public InputActionProperty Magnet1;
    public InputActionProperty Magnet2;
    public InputActionProperty Magnet3;

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
        PullObj.action?.Enable();
        PushObj.action?.Enable();
        QuickDash.action?.Enable();
        MagnetBut.action?.Enable();
        Magnet1.action?.Enable();
        Magnet2.action?.Enable();
        Magnet3.action?.Enable();
    }

    public void DisableInputs()
    {
        Horizontal.action?.Disable();
        Jump.action?.Disable();
        WallHold.action?.Disable();
        PullObj.action?.Disable();
        PushObj.action?.Disable();
        QuickDash.action?.Disable();
        MagnetBut.action?.Disable();
        Magnet1.action?.Disable();
        Magnet2.action?.Disable();
        Magnet3.action?.Disable();
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

    public bool PullObjPressed()
    {
        return PullObj.action?.IsPressed() ?? false;
    }
    public bool PushObjPressed()
    {
        return PushObj.action?.IsPressed() ?? false;
    }

    public bool QuickDashPressed()
    {
        return QuickDash.action?.WasPressedThisFrame() ?? false;
    }

    public bool MagnetButPressed()
    {
        return MagnetBut.action?.WasPressedThisFrame() ?? false;
    }

    public bool Mag1Pressed()
    {
        return Magnet1.action?.WasPressedThisFrame() ?? false;
    }
    public bool Mag2Pressed()
    {
        return Magnet2.action?.WasPressedThisFrame() ?? false;
    }
    public bool Mag3Pressed()
    {
        return Magnet3.action?.WasPressedThisFrame() ?? false;
    }
}