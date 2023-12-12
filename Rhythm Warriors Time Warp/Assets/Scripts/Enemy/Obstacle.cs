using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    /*
    public float obstacleSpeed = 5f; // speed at which the obstacle moves towards the player
    public int damageAmount = 10; // damage amount when the obstacle hits the player

    void Update()
    {
        MoveObstacle();
    }

    void MoveObstacle()
    {
        // move the obstacle towards the user
        transform.Translate(Vector3.back * obstacleSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // attach to the camera
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }

            Destroy(gameObject);
        }
    }
*/
}
