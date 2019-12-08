using System.Collections;
using UnityEngine;

public class Icicle : MonoBehaviour
{
    public float Damage        = 0.2f;
    public float FreezDuration = 0.5f;
    public bool DidDamage = false;

    private Vector3 originalPosition;
    private new Rigidbody2D rigidbody;

    private bool isDropping = false;

    void Start()
    {
        originalPosition = transform.position;
        rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(Drop());
    }

    public void Respawn(Collider2D other)
    {
        if (!other.CompareTag("Ground") || !isDropping)
            return;

        IEnumerator RespawnCoro()
        {
            isDropping = false;
            rigidbody.isKinematic = true;
            rigidbody.velocity = Vector2.zero;
            rigidbody.angularVelocity = 0;
            
            yield return new WaitForSeconds(0.5f);

            rigidbody.position = originalPosition;
            rigidbody.rotation = 0;
            DidDamage = false;
            StartCoroutine(Drop());
        }
        StartCoroutine(RespawnCoro());
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
        isDropping = true;
    }

//     private void OnCollisionEnter2D(Collision2D other) {
//         if (other.gameObject.CompareTag("Ground"))
//         {
//             StartCoroutine(Respawn());
//         }
//     }
}
