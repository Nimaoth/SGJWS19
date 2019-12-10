using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    public float TimeToReset = 3.0f;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            var playerController = other.attachedRigidbody.GetComponent<PlayerController>();
            playerController.Reload(true);
            Sandman.Instance.Sleep(gameObject, TimeToReset);
        }
    }
}
