using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowCannon : MonoBehaviour
{
    public GameObject SnowballPrefab;
    public Transform SpawnPoint;
    public float Strength = 1.0f;
    public float Interval = 2.0f;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(Interval);
        while (true)
        {
            var go = GameObject.Instantiate(SnowballPrefab, SpawnPoint.position, Quaternion.identity);
            var rb = go.GetComponent<Rigidbody2D>();
            rb.AddForce(SpawnPoint.up * Strength, ForceMode2D.Impulse);
            GameObject.Destroy(go, Interval * 5);

            yield return new WaitForSeconds(Interval);
        }
    }
}
