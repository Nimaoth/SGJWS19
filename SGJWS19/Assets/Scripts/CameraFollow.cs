using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float cameraSpeed = 0.1f;
    public float cameraZoomSpeed = 1.0f;

    private Rigidbody2D playerRigidbody;
    private new Camera camera;
    private float targetSize = 5;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = playerTransform.GetComponent<Rigidbody2D>();
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var diff = playerTransform.position - transform.position;
        diff.z = 0;
        transform.position = Vector3.Lerp(transform.position, transform.position + diff, cameraSpeed * Time.fixedDeltaTime);
        targetSize = Map(playerRigidbody.velocity.magnitude, 0, 10, 5, 10);
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetSize, cameraZoomSpeed * Time.fixedDeltaTime);
    }

    private float Map(float value, float f1, float f2, float t1, float t2) {
        value = Mathf.Clamp(value, f1, f2);
        return (value - f1) / (f2 - f1) * (t2 - t1) + t1;
    }
}
