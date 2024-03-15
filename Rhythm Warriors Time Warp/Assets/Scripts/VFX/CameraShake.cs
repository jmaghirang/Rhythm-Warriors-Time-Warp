using UnityEngine;

// some code referenced from:
// https://www.youtube.com/watch?v=9A9yj8KnM8c
// https://www.zulubo.com/gamedev/2019/1/5/vr-screen-shake-the-art-of-not-throwing-up

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform;

    // how long the camera shake effect will last
    public float shakeDuration = 0f;

    // the magnitude of the shake (how far the camera will move)
    public float shakeMagnitude = 0.7f;

    // the speed of the shake
    public float shakeSpeed = 1.0f;

    private Vector3 originalPosition;

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        originalPosition = cameraTransform.localPosition;
        
        Debug.Log("Camera Transform: " + cameraTransform.gameObject.name);
    }

    void Update()
    {
        // for testing purposes:
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            // start the camera shake when the space key is pressed
            StartShake(0.5f, 0.2f, 10.0f);
        }*/

        if (shakeDuration > 0)
        {
            // shake the camera
            cameraTransform.localPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;

            // reduce the shake duration over time
            shakeDuration -= Time.deltaTime * shakeSpeed;
        }
        else
        {
            // reset the camera position when the shake duration is over
            shakeDuration = 0f;
            cameraTransform.localPosition = originalPosition;
        }
    }

    // call this function to start the camera shake
    public void StartShake(float duration, float magnitude, float speed)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        shakeSpeed = speed;
    }
}