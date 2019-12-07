using UnityEngine;

public class Arm : MonoBehaviour
{
    public PlayerController player;

    public GameObject BulletPrefab;

    public float targetAngle;
    public bool shoot;
    public float cooldown = 0.0f;
    public float Energy;
    public bool charging = false;
    public float charge = 0.0f;

    private void Update()
    {
        var angle = Mathf.LerpAngle(transform.rotation.eulerAngles.z, targetAngle, player.ArmSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        cooldown -= Time.deltaTime;

        if (charging)
        {
            var energyBefore = Energy;
            Energy -= player.ChargeSpeed * Time.deltaTime;
            Energy = Mathf.Clamp01(Energy);

            charge += energyBefore - Energy;
        }
        else
        {
            Energy += player.RechargeSpeed * Time.deltaTime;
            Energy = Mathf.Clamp01(Energy);
        }
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
        var force = transform.up * player.ShotgunPower;
        player.Rigidbody.AddForceAtPosition(force, player.LeftForcePoint.position, ForceMode2D.Impulse);

        force.Normalize();

        for (int i = -3; i <= 3; i++)
        {
            var bulletForce = Quaternion.Euler(0, 0, i * 5 * Random.Range(0.9f, 1.1f)) * new Vector2(force.x, force.y);
            var bulletGO = GameObject.Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            bulletGO.GetComponent<Rigidbody2D>().AddForce(-bulletForce * player.BulletSpeed, ForceMode2D.Impulse);
        }
    }

    public void OnShootDown()
    {
        charging = true;
    }

    public void OnShootUp()
    {
        charging = false;
        shoot = true;
    }

    public void OnRotate(float angle)
    {
        targetAngle = angle;
    }
}