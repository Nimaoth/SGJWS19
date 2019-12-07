using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerControlSystem;

[Serializable]
public enum PlayerState
{
    Normal,
    Dead
}

public class PlayerController : MonoBehaviour, IPlayerControlsActions
{

    // public stuff
    public float ShotgunCooldown = 1.0f;
    public float ShotgunPower = 1.0f;
    public float ArmSpeed = 10.0f;
    public float BalanceStrength = 1.0f;
    public float BulletSpeed = 50.0f;
    public float HpLossSpeed = 0.1f;

    public Transform LeftForcePoint;
    public Transform RightForcePoint;
    public Rigidbody2D Rigidbody;

    public Arm Left;
    public Arm Right;
    public PlayerState State;
    public float HP { get; private set; } = 1.0f;

    // private stuff
    private PlayerControlSystem controls;

    [SerializeField]
    private bool isInBonfire = false;

    // Start is called before the first frame update
    void Start()
    {
        controls = new PlayerControlSystem();
        controls.PlayerControls.SetCallbacks(this);
        State = PlayerState.Normal;
    }

    private IEnumerator Death()
    {
        State = PlayerState.Dead;

        yield return new WaitForSeconds(1);

        var closestBonfire = Level.Instance.FindClosest(transform.position);
        
        Rigidbody.isKinematic     = true;
        yield return null;
        Rigidbody.position        = closestBonfire.transform.position;
        Rigidbody.velocity        = Vector2.zero; 
        Rigidbody.rotation        = 0;
        Rigidbody.angularVelocity = 0;
        yield return null;
        Rigidbody.isKinematic     = false;
        yield return new WaitForSeconds(1);
        State = PlayerState.Normal;
    } 

    private void Update()
    {
        if (isInBonfire)
        {
            HP += HpLossSpeed * 10 * Time.deltaTime;   
        }
        else
        {
            HP -= HpLossSpeed * Time.deltaTime;
            if (HP < 0.0f)
            {
                // die, respawn at closest unlocked bonfire
                StartCoroutine(Death());
            }
        }

        HP = Mathf.Clamp01(HP);
    }

    private void FixedUpdate()
    {
        var angle = (Rigidbody.rotation + 360.0f) % 360.0f;
        if (angle > 180.0f)
            angle -= 360.0f;

        // Rigidbody.MoveRotation(-angle * Time.fixedDeltaTime * BalanceStrength);
        Rigidbody.AddTorque(-angle / 180.0f * BalanceStrength);
    }

    private void Reload(bool force)
    {
        Left.Reload(force);
        Right.Reload(force);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bonfire"))
        {
            Level.Instance.VisitBonfire(other.GetComponent<Bonfire>());
            Level.Instance.Reset();
            isInBonfire = true;
        }
        else if (other.CompareTag("AmmoPack"))
        {
            Reload(true);
            other.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Bonfire"))
        {
            isInBonfire = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            // reload
            Reload(false);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            // reload
            Reload(false);
        }
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
