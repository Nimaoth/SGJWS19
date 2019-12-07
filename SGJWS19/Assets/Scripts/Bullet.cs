using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float LifeTimeMin = 2;
    public float LifeTimeMax = 4;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(gameObject, Random.Range(LifeTimeMin, LifeTimeMax));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
