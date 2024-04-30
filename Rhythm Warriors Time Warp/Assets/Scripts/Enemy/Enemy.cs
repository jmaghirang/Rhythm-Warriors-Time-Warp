using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

// Code referenced from
// https://youtu.be/ev0HsmgLScg?si=r4zVEzDwRoTi50C9

public class Enemy : Object
{
    double marginOfError;
    double audioTime;

    public GameObject vfx;

    // Start is called before the first frame update
    void Start()
    {
        // Get the time at which the enemy is instantiated
        timeInstantiated = SongManager.GetAudioSourceTime();
    }

    // Update is called once per frame
    void Update()
    {
        marginOfError = SongManager.instance.errorMargin;
        audioTime = SongManager.GetAudioSourceTime() - (SongManager.instance.inputDelay / 1000.0); // milliseconds to seconds

        // Current song time subtracted by the time enemy was instantiated
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;

        // note time is time between where the note spawns and where it is supposed to be hit; we also need the time between when it's supposed to be hit and when the object is supposed to despawn - hence the *2
        // t = 0 is the spawn location and t = 1 is the despawn location, so t = 0.5 is where the enemy is supposed to be hit by player
        float t = (float)(timeSinceInstantiated / (SongManager.instance.noteScreenTime * 2));

        // Set the enemy prefab mesh renderer to be false intially to contain visual anomalies causing issues with collisions
        GetComponent<MeshRenderer>().enabled = false;

        // If the enemy is past the point of where it's supposed to be hit...
        if (t > 0.5 + marginOfError)
        {
            // If the object passes where it is supposed to despawn
            Destroy(gameObject);

            // Instantiate(vfx, transform.position, Quaternion.identity);
            // Destroy(vfx);

            // This means player misses
            /*if (assignedTime + marginOfError < audioTime)
            {
                ScoreManager.instance.UpdateMisses(1);
            }*/

            ScoreManager.instance.UpdateMisses(1);
        }
        else
        {
            // Otherwise...
            // Move along two points in a straight line - from spawn point to despawn point
            transform.localPosition = Vector3.Lerp(Vector3.forward * SongManager.instance.noteSpawnZ, Vector3.forward * SongManager.instance.noteDespawnZ, t);

            // Enable the enemy prefab mesh to be visible
            GetComponent<MeshRenderer>().enabled = true;
        }
    }

    // Check collisions
    public override void OnTriggerEnter(Collider other)
    {
        // If the enemy collides with a game object tagged with 'weapon'...
        if (other.CompareTag("Weapon"))
        {
            if ((audioTime - assignedTime) < 0)
            {
                ScoreManager.instance.EarlyHit();

                Debug.Log("Early hit");

                VFXManager.instance.TriggerVFX(this);
                // Destroy enemy if it gets hit
                Destroy(gameObject);

                // Update score with score manager
                //ScoreManager scoreManager = FindObjectOfType<ScoreManager>(); // get reference
                if (ScoreManager.instance != null)
                {
                    Debug.Log("Hit accurate");
                    ScoreManager.instance.UpdateScore(50); // add one score when the weapon hits the enemy
                }
                else
                {
                    Debug.LogError("ScoreManager not found in the scene!"); // debugging
                }
            }
            else if (Math.Abs(audioTime - assignedTime) < marginOfError)
            {
                ScoreManager.instance.PerfectHit();

                Debug.Log($"Perfect! {Math.Abs(audioTime - assignedTime)} delay");

                VFXManager.instance.TriggerVFX(this);
                // Destroy enemy if it gets hit
                Destroy(gameObject);

                // Update score with score manager
                //ScoreManager scoreManager = FindObjectOfType<ScoreManager>(); // get reference
                if (ScoreManager.instance != null)
                {
                    Debug.Log("Hit accurate");
                    ScoreManager.instance.UpdateScore(100); // add one score when the weapon hits the enemy
                }
                else
                {
                    Debug.LogError("ScoreManager not found in the scene!"); // debugging
                }
            }
            else if ((audioTime - assignedTime) > 0)
            {
                ScoreManager.instance.LateHit();

                Debug.Log("Late hit");

                VFXManager.instance.TriggerVFX(this);
                // Destroy enemy if it gets hit
                Destroy(gameObject);

                // Update score with score manager
                //ScoreManager scoreManager = FindObjectOfType<ScoreManager>(); // get reference
                if (ScoreManager.instance != null)
                {
                    Debug.Log("Hit accurate");
                    ScoreManager.instance.UpdateScore(50); // add one score when the weapon hits the enemy
                }
                else
                {
                    Debug.LogError("ScoreManager not found in the scene!"); // debugging
                }
            }
            else
            {
                ScoreManager.instance.UpdateMisses(1);
                Debug.Log($"Hit inaccurate with {Math.Abs(audioTime - assignedTime)} delay");
            }
        }
    }
}