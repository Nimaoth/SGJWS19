using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayerUp : MonoBehaviour
{
    public new Transform camera;
    public PlayerController player;
    public Rigidbody2D playerRB;
    public GameObject UI;
    public GameObject PressStartToPlay;

    CameraFollow cf;
    private float zoomMin;
    private float zoomMax;

    private void Start() {
        cf = camera.GetComponent<CameraFollow>();
        zoomMin = cf.ZoomMin;
        zoomMax = cf.ZoomMax;
        cf.ZoomMin = cf.ZoomMax = 4;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")){
            var move = new Vector2(0, 12);

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
            UI.SetActive(true);
            PressStartToPlay.SetActive(false);
            cf.ZoomMin = zoomMin;
            cf.ZoomMax = zoomMax;
        }
    }
}
