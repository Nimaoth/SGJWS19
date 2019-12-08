using System.Collections;
using UnityEngine;

public class Icicle : MonoBehaviour
{
    public float Damage        = 0.2f;
    public float FreezDuration = 0.5f;
    public bool DidDamage = false;

    private Vector3 originalPosition;
    private new Rigidbody2D rigidbody;

    void Start()
    {
        originalPosition = transform.position;
        rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(Drop());
    }

    private void Respawn()
    {
        DidDamage = false;
        rigidbody.isKinematic = true;
        rigidbody.position = originalPosition;
        rigidbody.rotation = 0;
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0;
        StartCoroutine(Drop());
    }

    private IEnumerator Drop()
    {
        rigidbody.isKinematic = true;
        float start = Time.time;
        while (Time.time < start + 2.0f)
        {
            float s = (Time.time - start) / 2.0f;
            transform.localScale = new Vector3(s, s, s);
            yield return null;
        }
        rigidbody.isKinematic = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Ground"))
        {
            Respawn();
        }
    }

//     private void OnCollisionEnter2D(Collision2D other) {
//         if (other.gameObject.CompareTag("Ground"))
//         {
//             StartCoroutine(Respawn());
//         }
//     }
}
