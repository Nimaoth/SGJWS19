using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance;

    public Transform playerTransform;
    public float cameraSpeed = 0.1f;
    public float cameraZoomSpeed = 1.0f;

    public float ZoomMin = 3.0f;
    public float ZoomMax = 6.0f;

    public float ShakeDuration = 0.2f;
    public float ShakeIntensity = 0.2f;

    private Rigidbody2D playerRigidbody;
    private new Camera camera;
    private float targetSize = 5;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        playerRigidbody = playerTransform.GetComponent<Rigidbody2D>();
        camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var diff = playerTransform.position - transform.position;
        diff.z = 0;
        var pos = Vector3.Lerp(transform.position, transform.position + diff, cameraSpeed * Time.fixedDeltaTime);
        pos.z = -10;
        transform.position = pos;
        targetSize = Map(playerRigidbody.velocity.magnitude, 0, 10, ZoomMin, ZoomMax);
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetSize, cameraZoomSpeed * Time.fixedDeltaTime);
    }

    private float Map(float value, float f1, float f2, float t1, float t2) {
        value = Mathf.Clamp(value, f1, f2);
        return (value - f1) / (f2 - f1) * (t2 - t1) + t1;
    }

    public void ShakeDaBooty()
    {
        StartCoroutine(ShakeyShakey());
    }

    private IEnumerator ShakeyShakey()
    {
        float start = 0;
        float radius = ShakeIntensity;
        while (start < ShakeDuration)
        {
            camera.transform.localPosition = Random.insideUnitCircle.normalized * radius;

            radius *= 0.9f;
            start += Time.deltaTime;
            yield return null;
        }
        
        camera.transform.localPosition = Vector3.zero;
    }
}
