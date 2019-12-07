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
        rigidbody.isKinematic = true;
        rigidbody.position = originalPosition;
        rigidbody.rotation = 0;
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0;
    }

    private IEnumerator Drop()
    {
        while (true) {
            Respawn();
            yield return new WaitForSeconds(2.0f);
            rigidbody.isKinematic = false;
            DidDamage = false;
            yield return new WaitForSeconds(4.0f);
        }
    }

//     private void OnCollisionEnter2D(Collision2D other) {
//         if (other.gameObject.CompareTag("Ground"))
//         {
//             StartCoroutine(Respawn());
//         }
//     }
}
