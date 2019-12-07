using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    public float Damage = 0.2f;
    public float SplitForce = 1.0f;
    public GameObject PiecePrefab;

    [SerializeField]
    private float breakRadius;

    private void Start()
    {
        var collider = GetComponent<Collider2D>();
        breakRadius = collider.bounds.extents.magnitude;
    }

    public void Break()
    {
        gameObject.SetActive(false);
        GameObject.Destroy(gameObject);

        int count = 6;

        // create sub objects
        for (int y = 0; y < count; y++)
        {
            for (int x = 0; x < count; x++)
            {
                float xOff = (x - count + 0.5f) / (count - 0.5f) * breakRadius;
                float yOff = (y - count + 0.5f) / (count - 0.5f) * breakRadius;

                var pieceGO = GameObject.Instantiate(PiecePrefab, transform.position + new Vector3(xOff, yOff, 0), Quaternion.identity);
                GameObject.Destroy(pieceGO, Random.Range(1.0f, 3.0f));
                var rb = pieceGO.GetComponent<Rigidbody2D>();
                rb.AddForce(Random.insideUnitCircle * SplitForce, ForceMode2D.Impulse);
            }
        }
    }
}
