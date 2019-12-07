using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerControlSystem;

[Serializable]
public enum PlayerState
{
    Normal,
    Frozen,
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
    public float HpRegenSpeed = 5.0f;

    public Transform LeftForcePoint;
    public Transform RightForcePoint;
    public Rigidbody2D Rigidbody;

    public Arm Left;
    public Arm Right;
    public HingeJoint2D HeadJoint;

    public PlayerState State;
    public float HP { get; private set; } = 1.0f;

    // private stuff
    private PlayerControlSystem controls;

    [SerializeField]
    private bool isInBonfire = false;

    [SerializeField]
    private bool isOnGround = false;

    [SerializeField]
    private float freezeTimeLeft = 0.0f;

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
        freezeTimeLeft = 0.0f;

        yield return new WaitForSeconds(1);

        var closestBonfire = Level.Instance.FindClosest(transform.position);
        
        Rigidbody.isKinematic     = true;
        yield return null;
        Rigidbody.position        = closestBonfire.SpawnPoint.position;
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
        // handle freezing
        freezeTimeLeft -= Time.deltaTime;
        if (freezeTimeLeft <= 0.0f)
        {
            // unfreeze
            State = PlayerState.Normal;
        }

        // handle damage/healing
        if (isInBonfire)
            TakeDamage(-HpRegenSpeed * Time.deltaTime);
        else
            TakeDamage(HpLossSpeed * Time.deltaTime);
    }

    private void Freeze(float duration)
    {
        switch (State) {
            case PlayerState.Frozen:
                freezeTimeLeft = Mathf.Max(duration, freezeTimeLeft);
                break;

            case PlayerState.Normal:
                State = PlayerState.Frozen;
                freezeTimeLeft = duration;
                break;
        }
    }

    private void TakeDamage(float amount)
    {
        HP -= amount;
        if (HP < 0.0f)
        {
            // die, respawn at closest unlocked bonfire
            StartCoroutine(Death());
        }
        HP = Mathf.Clamp01(HP);
    }

    private void FixedUpdate()
    {
        switch (State) {
            case PlayerState.Normal:
                if (Rigidbody.velocity.sqrMagnitude < 0.5)
                {
                    var angle = Mathf.LerpAngle(Rigidbody.rotation, 0, BalanceStrength * Time.fixedDeltaTime);
                    Rigidbody.MoveRotation(angle);
                }
                break;
        }
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
            freezeTimeLeft = 0.0f;
        }
        else if (other.CompareTag("AmmoPack"))
        {
            Reload(true);
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Icicle"))
        {
            var icicle = other.gameObject.GetComponent<Icicle>();
            if (!icicle.DidDamage)
            {
                TakeDamage(icicle.Damage);
                Freeze(icicle.FreezDuration);
            }
            icicle.DidDamage = true;
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
            isOnGround = true;
            Reload(false);
        }
        else if (other.gameObject.CompareTag("Snowball"))
        {
            var snowball = other.gameObject.GetComponent<Snowball>();
            TakeDamage(snowball.Damage);
            Freeze(snowball.FreezDuration);
            snowball.Break();
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            Reload(false);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
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
