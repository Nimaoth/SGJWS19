using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider sPlayerHP;

    public GameObject[] AmmoLeft;
    public GameObject[] AmmoRight;

    public PlayerController playerController;

    public float VisibleHPAdjustSpeed = 10.0f;

    private float visibleHP;

    // Update is called once per frame
    void Update()
    {
        visibleHP = Mathf.Lerp(visibleHP, playerController.HP, VisibleHPAdjustSpeed * Time.deltaTime);
        sPlayerHP.value = visibleHP;

        for (int i = 0; i < AmmoLeft.Length; i++)
            AmmoLeft[i].SetActive(i < playerController.Left.Ammo);

        for (int i = 0; i < AmmoRight.Length; i++)
            AmmoRight[i].SetActive(i < playerController.Right.Ammo);
    }
}
