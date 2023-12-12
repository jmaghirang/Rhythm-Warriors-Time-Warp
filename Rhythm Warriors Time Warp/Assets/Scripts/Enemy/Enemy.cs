using UnityEngine;

// Code referenced from
// https://youtu.be/ev0HsmgLScg?si=r4zVEzDwRoTi50C9

public class Enemy : Object
{
    public bool canBeHit;

    // Start is called before the first frame update
    void Start()
    {
        timeInstantiated = SongManager.GetAudioSourceTime();
        GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;

        // note time is time between where the note spawns and where it is supposed to be hit; we also need the time between when it's supposed to be hit and when the object is supposed to despawn - hence the *2
        // t = 0 is the spawn location and t = 1 is the despawn location
        float t = (float)(timeSinceInstantiated / (SongManager.instance.noteScreenTime * 2));

        if (t > 0.5)
        {
            // If the object passes where it is supposed to despawn
            // Player misses
            Destroy(gameObject);

            AudioManager.instance.missSFX.Play();
            Debug.Log("Miss");
        }
        else
        {
            // Move along two points - from spawn point to despawn point
            transform.localPosition = Vector3.Lerp(Vector3.forward * SongManager.instance.noteSpawnZ, Vector3.forward * SongManager.instance.noteDespawnZ, t);
            GetComponentInChildren<MeshRenderer>().enabled = true;
        }
    }

    // Check collisions with hit box
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            canBeHit = true;

            Destroy(gameObject);

            AudioManager.instance.hitSFX.Play();
            Debug.Log("Hit");

            // Score Manager
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