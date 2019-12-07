using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider sEnergyLeft;
    public Slider sEnergyRight;

    public PlayerController playerController;

    // Update is called once per frame
    void Update()
    {
        sEnergyLeft.value  = 1 - Mathf.Clamp(playerController.Left.cooldown, 0, playerController.ShotgunCooldown) / playerController.ShotgunCooldown;
        sEnergyRight.value = 1 - Mathf.Clamp(playerController.Right.cooldown, 0, playerController.ShotgunCooldown) / playerController.ShotgunCooldown;
    }
}
