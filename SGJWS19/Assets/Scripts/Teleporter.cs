using UnityEngine;
using UnityEngine.InputSystem;

public class Teleporter : MonoBehaviour
{
    public PlayerController Player;
    public Transform[] Teleports;

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard.spaceKey.wasPressedThisFrame) Level.Instance.Reset();

        if (keyboard.f1Key.wasPressedThisFrame) Teleport(0);
        if (keyboard.f2Key.wasPressedThisFrame) Teleport(1);
        if (keyboard.f3Key.wasPressedThisFrame) Teleport(2);
        if (keyboard.f4Key.wasPressedThisFrame) Teleport(3);
        if (keyboard.f5Key.wasPressedThisFrame) Teleport(4);
        if (keyboard.f6Key.wasPressedThisFrame) Teleport(5);
        if (keyboard.f7Key.wasPressedThisFrame) Teleport(6);
        if (keyboard.f8Key.wasPressedThisFrame) Teleport(7);
        if (keyboard.f9Key.wasPressedThisFrame) Teleport(8);
        if (keyboard.f10Key.wasPressedThisFrame) Teleport(9);
        if (keyboard.f11Key.wasPressedThisFrame) Teleport(10);
        if (keyboard.f12Key.wasPressedThisFrame) Teleport(11);
    }

    private void Teleport(int index)
    {
        if (index < 0 || index >= Teleports.Length)
            return;
        Player.TeleportTo(Teleports[index].position);

        Level.Instance.Reset();
    }
}
