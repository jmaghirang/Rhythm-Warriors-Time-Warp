using UnityEngine;

public class EnemySlicing : MonoBehaviour
{
    public GameObject slicedEnemyPrefab; // Reference to the sliced enemy parts prefab
    public float sliceForce = 100f; // Force applied to sliced parts

    // Collision detection with player's weapon
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon")) // Assuming the player's weapon has the "Weapon" tag
        {
            SliceEnemy();
        }
    }

    void SliceEnemy()
    {
        // Instantiate sliced enemy parts
        GameObject slicedEnemy = Instantiate(slicedEnemyPrefab, transform.position, transform.rotation);

        // Get rigidbodies from sliced parts
        Rigidbody[] rigidbodies = slicedEnemy.GetComponentsInChildren<Rigidbody>();

        // Apply force to sliced parts
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.AddForce(transform.forward * sliceForce);
        }

        // Destroy the original enemy
        Destroy(gameObject);
    }
}