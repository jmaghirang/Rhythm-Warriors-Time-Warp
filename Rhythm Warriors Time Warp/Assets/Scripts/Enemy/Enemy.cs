using UnityEngine;

// Code referenced from
// https://youtu.be/ev0HsmgLScg?si=r4zVEzDwRoTi50C9

public class Enemy : Object
{
    // If the enemy can be hit - i don't think this does anything rn
    public bool canBeHit;

    // Start is called before the first frame update
    void Start()
    {
        // Get the time at which the enemy is instantiated
        timeInstantiated = SongManager.GetAudioSourceTime();
        // Set the enemy prefab mesh renderer to be false intially to contain visual anomalies
        GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Current song time subtracted by the time enemy was instantiated
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;

        // note time is time between where the note spawns and where it is supposed to be hit; we also need the time between when it's supposed to be hit and when the object is supposed to despawn - hence the *2
        // t = 0 is the spawn location and t = 1 is the despawn location, so t = 0.5 is where the enemy is supposed to be hit by player
        float t = (float)(timeSinceInstantiated / (SongManager.instance.noteScreenTime * 2));

        // If the enemy is past the point of where it's supposed to be hit...
        if (t > 0.5)
        {
            // If the object passes where it is supposed to despawn
            Destroy(gameObject);

            // This means player misses
            AudioManager.instance.missSFX.Play();
            GameManager.instance.missCounter++;
            Debug.Log("Miss");
        }
        else
        {
            // Otherwise...
            // Move along two points - from spawn point to despawn point
            transform.localPosition = Vector3.Lerp(Vector3.forward * SongManager.instance.noteSpawnZ, Vector3.forward * SongManager.instance.noteDespawnZ, t);
            // Enable the enemy prefab mesh to be visible
            GetComponentInChildren<MeshRenderer>().enabled = true;
        }
    }

    // Check collisions
    public override void OnTriggerEnter(Collider other)
    {
        // If the enemy collides with a game object tagged with 'weapon'...
        if (other.CompareTag("Weapon"))
        {
            canBeHit = true;

            // Destroy enemy if it gets hit
            Destroy(gameObject);

            AudioManager.instance.hitSFX.Play();
            Debug.Log("Hit");

            // Update score with score manager
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>(); // get reference
            if (scoreManager != null)
            {
                scoreManager.UpdateScore(1); // add one score when the weapon hits the enemy
            }
            else
            {
                Debug.LogError("ScoreManager not found in the scene!"); // debugging
            }
        }
    }
}