using System.Collections;
using UnityEngine;

public class Icicle : MonoBehaviour
{
    public float Damage        = 0.2f;
    public float FreezDuration = 0.5f;
    public bool DidDamage = false;

    private Vector3 originalPosition;
    private bool respawning;
    private new Rigidbody2D rigidbody;

    void Start()
    {
        originalPosition = transform.position;
        rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(Drop());
    }

    private IEnumerator Respawn()
    {
        if (respawning)
            yield break;
        respawning = true;
        yield return new WaitForSeconds(2.0f);

        rigidbody.isKinematic = true;
        rigidbody.position = originalPosition;
        rigidbody.rotation = 0;
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0;
        respawning = false;

        StartCoroutine(Drop());
    }

    private IEnumerator Drop()
    {
        yield return new WaitForSeconds(Random.Range(2.0f, 4.0f));
        rigidbody.isKinematic = false;
        DidDamage = false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(Respawn());
        }
    }
}
