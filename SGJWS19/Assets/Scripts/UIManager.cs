using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider sEnergyLeft;
    public Slider sEnergyRight;
    public Slider sChargeLeft;
    public Slider sChargeRight;

    public PlayerController playerController;

    // Update is called once per frame
    void Update()
    {
        sEnergyLeft.value = playerController.Left.Energy;
        sEnergyRight.value = playerController.Right.Energy;
        sChargeLeft.value = playerController.Left.charge;
        sChargeRight.value = playerController.Right.charge;
    }
}
