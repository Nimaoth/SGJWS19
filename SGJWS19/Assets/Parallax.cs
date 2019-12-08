using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public new Transform camera;
    public float speedCoefficient;
    Vector3 lastpos;

    void Start () {
        lastpos = camera.position;
    }

    void Update () {
        transform.position -= ((lastpos - camera.position)*speedCoefficient);
        lastpos = camera.position;
    }
}
