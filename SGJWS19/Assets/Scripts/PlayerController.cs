using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerControlSystem;

[Serializable]
public enum PlayerState
{
    Intro,
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
    public float ReloadDelay = 0.75f;

    public Transform LeftForcePoint;
    public Transform RightForcePoint;
    public Rigidbody2D Rigidbody;
    public SpriteRenderer[] Sprites;
    public Color freezeColor;

    public Arm Left;
    public Arm Right;
    public AudioSource ReloadSound;

    public PlayerState State = PlayerState.Normal;

    public bool CanFreeze = false;

    public float HP { get; private set; } = 1.0f;

    // private stuff
    private PlayerControlSystem controls;

    [SerializeField]
    private bool isInBonfire = false;

    [SerializeField]
    private bool isOnGround = false;

    [SerializeField]
    private float groundTime = 0.0f;

    [SerializeField]
    private float freezeTimeLeft = 0.0f;

    private bool isReloading = false;

    public ParticleSystem footfallSnow;

    // Start is called before the first frame update
    void Start()
    {
        controls = new PlayerControlSystem();
        controls.PlayerControls.SetCallbacks(this);
    }

    internal void TeleportTo(Vector3 position, bool instaMoveCam = false)
    {
        IEnumerator MoveTo()
        {
            Rigidbody.isKinematic     = true;

            yield return null;

            Rigidbody.transform.position = position;
            Rigidbody.isKinematic     = true;
            Rigidbody.position        = position;
            Rigidbody.rotation        = 0;
            Rigidbody.velocity        = Vector2.zero; 
            Rigidbody.angularVelocity = 0;
            yield return null;

            yield return null;
            Rigidbody.isKinematic     = false;
        }
        StartCoroutine(MoveTo());
    }

    public void EnableFreeze(Collider2D _) {
        CanFreeze = true;
    }

    private IEnumerator Death()
    {
        State = PlayerState.Dead;

        yield return new WaitForSeconds(1);

        var closestBonfire = Level.Instance.FindClosest(transform.position);

        TeleportTo(closestBonfire.SpawnPoint.position);
        yield return new WaitForSeconds(1);
        State = PlayerState.Normal;
    } 

    private void Update()
    {
        if (State == PlayerState.Intro)
            return;

        if (isOnGround)
            groundTime += Time.deltaTime;
        else
            groundTime = 0.0f;

        // handle freezing
        switch (State) {
            case PlayerState.Frozen:
            case PlayerState.Dead:
                freezeTimeLeft -= Time.deltaTime;
                if (freezeTimeLeft <= 0.0f)
                    State = PlayerState.Normal;
                break;
        }

        if (CanFreeze){
            // handle damage/healing
            if (isInBonfire)
                TakeDamage(-HpRegenSpeed * Time.deltaTime);
            else
                TakeDamage(HpLossSpeed * Time.deltaTime);
        }

        var targetColor = Color.white;
        switch (State) {
            case PlayerState.Frozen:
            case PlayerState.Dead:
                targetColor = freezeColor;
                break;
        }

        foreach (var sprite in Sprites)
            sprite.color = Color.Lerp(sprite.color, targetColor, Time.deltaTime * 10.0f);
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
        if (HP <= 0.0f)
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
                if (Rigidbody.velocity.sqrMagnitude < 1.5 && isOnGround)
                {
                    var angle = Mathf.LerpAngle(Rigidbody.rotation, 0, BalanceStrength * Time.fixedDeltaTime);
                    Rigidbody.MoveRotation(angle);
                }
                break;
        }
    }

    public void Reload(bool force)
    {
        if (force)
        {
            var l = Left.Reload();
            var r = Right.Reload();
            ReloadSound.Play();
            return;
        }

        if (isReloading)
            return;
        isReloading = true;

        IEnumerator ReloadCoro()
        {
            yield return new WaitForSeconds(ReloadDelay);
            if (groundTime >= ReloadDelay)
            {
                var l = Left.Reload();
                var r = Right.Reload();
                if (!ReloadSound.isPlaying && (l || r))
                    ReloadSound.Play();
            }

            isReloading = false;
        }
        StartCoroutine(ReloadCoro());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bonfire"))
        {
            Level.Instance.VisitBonfire(other.GetComponent<Bonfire>());
            Level.Instance.Reset();
            isInBonfire = true;
            freezeTimeLeft = Mathf.Min(1.0f, freezeTimeLeft);
        }
        else if (other.gameObject.CompareTag("Icicle"))
        {
            var icicle = other.gameObject.GetComponent<Icicle>();
            if (!icicle.DidDamage)
            {
                Freeze(icicle.FreezDuration);
                TakeDamage(icicle.Damage);
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

            ParticleSystem.EmitParams param = new ParticleSystem.EmitParams();            
            param.startSize = UnityEngine.Random.Range(0.05f, 0.15f);
            param.startLifetime = 1.5f;

            for(int i = 0; i < 10; i++)
            {
                param.velocity = new Vector3(UnityEngine.Random.Range(-1f, 1f), 1f, 0);
                footfallSnow.Emit(param, 1);
            }



        }
        else if (other.gameObject.CompareTag("Snowball"))
        {
            var snowball = other.gameObject.GetComponent<Snowball>();
            Freeze(snowball.FreezDuration);
            TakeDamage(snowball.Damage);
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

    public void OnAdvanceDialog(InputAction.CallbackContext context)
    {
        if (context.performed)
            DialogSystem.Instance.AdvanceDialog();
    }

    #endregion
}
