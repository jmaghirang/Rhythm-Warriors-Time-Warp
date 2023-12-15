using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code referenced from
// https://youtu.be/ev0HsmgLScg?si=r4zVEzDwRoTi50C9

public class Obstacle : Object
{
    public int damageAmount; // How much damage the obstacle will do
    public bool instanceOfDmg = false; // Debugging purposes - I don't think this does anything

    // Start is called before the first frame update
    void Start()
    {
        // Get the time at which the obstacle is instantiated
        timeInstantiated = SongManager.GetAudioSourceTime();
        // Set the obstacle prefab mesh renderer to be false/not visible intially to contain visual anomalies
        GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Current song time subtracted by the time obstacle was instantiated
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;

        // note time is time between where the note spawns and where it is supposed to be hit; we also need the time between when it's supposed to be hit and when the object is supposed to despawn - hence the *2
        // t = 0 is the spawn location and t = 1 is the despawn location, so t = 0.5 is where the obstacle is supposed to be hit by player
        float t = (float)(timeSinceInstantiated / (SongManager.instance.noteScreenTime * 2));

        if (t > 1)
        {
            // If the object passes where it is supposed to, then despawn
            Destroy(gameObject);

        }
        else
        {
            // Otherwise...
            // Move along two points - from spawn point to despawn point
            transform.localPosition = Vector3.Lerp(Vector3.forward * SongManager.instance.noteSpawnZ, Vector3.forward * SongManager.instance.noteDespawnZ, t);
            // Enable the obstacle prefab mesh to be visible
            GetComponentInChildren<MeshRenderer>().enabled = true;
        }
    }

    // Check collisions
    public override void OnTriggerEnter(Collider other)
    {
        // If the obstacle collides with object tagged 'player'
        if (other.CompareTag("Player"))
        {
            // Decrease the player's health
            var p = other.GetComponent<Player>();
            if (p.currentHealth != 0)
            {
                if (!instanceOfDmg)
                {
                    p.TakeDamage(damageAmount);

                    Debug.Log("Current health: " + p.currentHealth);
                    Debug.Log("Player hit once");

                    instanceOfDmg = true;
                }

                AudioManager.instance.missSFX.Play();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            instanceOfDmg = false;
        }
    }
}
