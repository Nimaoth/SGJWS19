using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerControlSystem;

[Serializable]
public enum PlayerState
{
    Normal
}

public class PlayerController : MonoBehaviour, IPlayerControlsActions
{

    // public stuff
    public float ShotgunCooldown = 1.0f;
    public float ShotgunPower = 1.0f;
    public float ArmSpeed = 10.0f;
    public float RechargeSpeed = 0.5f;
    public float ChargeSpeed = 0.5f;
    public Transform LeftForcePoint;
    public Transform RightForcePoint;
    public Rigidbody2D Rigidbody;

    public Arm Left;
    public Arm Right;
    public PlayerState State;

    // private stuff
    private PlayerControlSystem controls;

    // Start is called before the first frame update
    void Start()
    {
        controls = new PlayerControlSystem();
        controls.PlayerControls.SetCallbacks(this);
        State = PlayerState.Normal;
    }

    #region Input stuff

    public void OnLeftShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Left.OnShootDown();
        }
        else if (context.canceled)
        {
            Left.OnShootUp();
        }
    }

    public void OnRightShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Right.OnShootDown();
        }
        else if (context.canceled)
        {
            Right.OnShootUp();
        }
    }

    public void OnRotateLeftArm(InputAction.CallbackContext context)
    {
        var dir = context.ReadValue<Vector2>();
        if (dir.sqrMagnitude > 0.1)
        {
            float angle = Vector2.SignedAngle(Vector2.down, dir);
            Left.OnRotate(angle);
        }
        
    }

    public void OnRotateRightArm(InputAction.CallbackContext context)
    {
        var dir = context.ReadValue<Vector2>();
        if (dir.sqrMagnitude > 0.1)
        {
            float angle = Vector2.SignedAngle(Vector2.down, dir);
            Right.OnRotate(angle);
        }
    }

    #endregion
}
