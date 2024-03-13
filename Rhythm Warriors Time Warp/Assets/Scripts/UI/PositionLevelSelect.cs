using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// code from valem tutorials

public class PositionLevelSelect : MonoBehaviour
{
    public float distanceFromPlayer = 0.5f;
    public float verticalOffset = -0.5f;
    public Transform playerCamera;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 direction = playerCamera.forward;
        direction.y = 0;
        direction.Normalize();

        Vector3 targetPosition = playerCamera.position + direction * distanceFromPlayer + Vector3.up * verticalOffset;

        gameObject.transform.position = targetPosition;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(playerCamera.position);

        gameObject.transform.forward *= -1;
    }
}
