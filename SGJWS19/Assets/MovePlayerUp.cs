using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayerUp : MonoBehaviour
{
    public new Transform camera;
    public PlayerController player;
    public Rigidbody2D playerRB;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")){
            var move = new Vector2(0, 10);

            camera.Translate(move);
            playerRB.isKinematic = true;
            playerRB.position = playerRB.position + move;
            playerRB.transform.position = playerRB.position;
            playerRB.isKinematic = false;
        }
    }

    public void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
            return; // No gamepad connected.

        if (gamepad.startButton.wasPressedThisFrame)
        {
            GameObject.Destroy(gameObject);
            player.State = PlayerState.Normal;
        }
    }
}
