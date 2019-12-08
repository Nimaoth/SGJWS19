using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    private Vector3 originalPosition;
    private new Rigidbody2D rigidbody;

    void Start()
    {
        originalPosition = transform.position;
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector3(0, 0, 0);
    }

    private void Respawn()
    {
        rigidbody.position = originalPosition;
        rigidbody.velocity = new Vector3(0,0,0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Rezone"))
        {
            Respawn();
        }
    }
}
