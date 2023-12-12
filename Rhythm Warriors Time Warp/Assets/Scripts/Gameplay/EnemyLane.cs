using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

// Code referenced from
// https://youtu.be/ev0HsmgLScg?si=r4zVEzDwRoTi50C9

public class EnemyLane : Lane
{
    public List<Enemy> enemies = new();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpawnObjects();


        // Hit Registration - Player Accuracy (in progress still testing)

        /*if (timeIndex < timeStamps.Count)
        {
            // Assigned variables for clarity
            double timeStamp = timeStamps[timeIndex];
            double marginOfError = SongManager.instance.errorMargin;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.instance.inputDelay / 1000.0); // milliseconds to seconds

            // If the player hit within the margin of error
            if (Math.Abs(audioTime - timeStamp) < marginOfError)
            {
                Hit();
                // Notify of accurate hit
                print($"Hit on {timeIndex} note");

                // Destroy enemy game object
                Destroy(enemies[timeIndex].gameObject);
                // Increment index to next time stamp that needs to be fulfilled
                timeIndex++;
            }
            else
            {
                // Notify of inaccurate hit
                print($"Hit inaccurate on {timeIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
            }
            //}

            // If the player did not hit the object within the margin of error at the specified time stamp
            if (timeStamp + marginOfError <= audioTime)
            {
                Miss();
                print($"Missed {timeIndex} note");
                timeIndex++;
            }
        }*/
    }

    public override void SpawnObjects()
    {
        if (spawnIndex < timeStamps.Count && SongManager.instance.audioSource.isPlaying)
        {
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.instance.noteScreenTime)
            {
                // Debug.Log("Spawn Index: " + spawnIndex + "\n");

                // Spawn enemy
                var note = Instantiate(prefab, transform);
                Debug.Log("Enemy Spawned");

                // Add spawned enemy to list
                enemies.Add(note.GetComponent<Enemy>());

                // Enemy will know where to position itself so the player can hit
                note.GetComponent<Enemy>().assignedTime = (float)timeStamps[spawnIndex];
                // Debug.Log("Assigned Time: " + timeStamps[spawnIndex] + "\n");

                // Move on to next enemy to be spawned
                spawnIndex++;
            }
        }
    }

    public override void RefreshLane()
    {
        enemies.Clear();
        timeStamps.Clear();

        foreach (Transform child in transform) Destroy(child.gameObject);
    }

    // Used when accuracy is implemented
    /*private void Hit()
    {
        AudioManager.instance.hitSFX.Play();
    }

    private void Miss()
    {
        AudioManager.instance.missSFX.Play();
    }*/
}