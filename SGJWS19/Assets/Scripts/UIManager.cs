using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider sPlayerHP;

    public GameObject[] AmmoLeft;
    public GameObject[] AmmoRight;

    public PlayerController playerController;

    // Update is called once per frame
    void Update()
    {
        sPlayerHP.value = playerController.HP;

        for (int i = 0; i < AmmoLeft.Length; i++)
            AmmoLeft[i].SetActive(i < playerController.Left.Ammo);

        for (int i = 0; i < AmmoRight.Length; i++)
            AmmoRight[i].SetActive(i < playerController.Right.Ammo);
    }
}
