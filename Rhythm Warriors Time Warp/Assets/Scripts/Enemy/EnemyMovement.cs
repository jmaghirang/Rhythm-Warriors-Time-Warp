using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed = 3f; // Speed of enemy movement
    private Transform player; // Reference to the player's transform

    void Start()
    {
        // Find the player by tag (Assuming the player has the "Player" tag)
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Move towards the player
        transform.position = Vector3.MoveTowards(transform.position, player.position, movementSpeed * Time.deltaTime);

        // Rotate to face the player (optional)
        Vector3 direction = player.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    // Collision detection with player (Modify this based on your interaction system)
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Handle player-enemy interaction (e.g., reduce player health)
            // For simplicity, destroying the enemy on collision
            Destroy(gameObject);
        }
    }
}