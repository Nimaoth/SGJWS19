using UnityEngine;

public class Arm : MonoBehaviour
{
    public PlayerController player;

    public float targetAngle;
    public bool shoot;
    public float cooldown = 0.0f;
    public float Energy;
    public bool charging = false;
    public float charge = 0.0f;

    private void Update()
    {
        var leftAngle = Mathf.LerpAngle(transform.rotation.eulerAngles.z, targetAngle, player.ArmSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, leftAngle);

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
                if (shoot)
                {
                    if (cooldown <= 0.0f)
                    {
                        var dir = transform.up;
                        player.Rigidbody.AddForceAtPosition(dir * player.ShotgunPower * charge, player.LeftForcePoint.position, ForceMode2D.Impulse);
                        cooldown = player.ShotgunCooldown;
                    }
                    charge = 0;
                }
                break;
            }
        }

        shoot = false;
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
