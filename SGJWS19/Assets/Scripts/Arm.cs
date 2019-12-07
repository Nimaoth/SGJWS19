using UnityEngine;

public class Arm : MonoBehaviour
{
    public PlayerController player;

    public GameObject BulletPrefab;
    public GameObject SmokeScreen;
    public GameObject SmokeSpawnPoint;

    public float targetAngle;
    public bool shoot;
    public float cooldown = 0.0f;

    public int Ammo = 2;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        switch (player.State)
        {
            case PlayerState.Normal:
            {
                var angle = Mathf.LerpAngle(transform.rotation.eulerAngles.z, targetAngle, player.ArmSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            break;
        }

        cooldown -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        switch (player.State)
        {
            case PlayerState.Normal:
            {
                if (shoot && cooldown <= 0.0f)
                {
                    Shoot();
                    cooldown = player.ShotgunCooldown;
                }
                break;
            }
        }

        shoot = false;
    }

    private void Shoot()
    {
        if (Ammo == 0)
        {
            // maybe play a sound so we know we have no ammo
            return;
        }

        var smoke = GameObject.Instantiate(SmokeScreen, SmokeSpawnPoint.transform.position, Quaternion.identity);
        GameObject.Destroy(smoke, 2);
        CameraFollow.Instance.ShakeDaBooty();
        audioSource.Play();
        Ammo -= 1;

        var force = transform.up * player.ShotgunPower;
        player.Rigidbody.AddForceAtPosition(force, player.LeftForcePoint.position, ForceMode2D.Impulse);

        force.Normalize();

        for (int i = -3; i <= 3; i++)
        {
            var bulletForce = Quaternion.Euler(0, 0, i * 5 * Random.Range(0.9f, 1.1f)) * new Vector2(force.x, force.y);
            var bulletGO = GameObject.Instantiate(BulletPrefab, transform.position, Quaternion.identity);

            bulletGO.GetComponent<Rigidbody2D>().velocity = -bulletForce * player.BulletSpeed;
        }
    }

    public void Reload(bool force)
    {
        if (force || cooldown <= 0)
            Ammo = 2;
    }

    public void OnShootDown()
    {
        shoot = true;
    }

    public void OnShootUp()
    {
    }

    public void OnRotate(float angle)
    {
        targetAngle = angle;
    }
}
