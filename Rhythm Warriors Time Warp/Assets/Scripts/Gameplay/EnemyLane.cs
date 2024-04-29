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
    // List of enemies in a scene
    public List<Enemy> enemies = new();

    public string enemyType;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Spawn enemies
        SpawnObjects();

        // Player Accuracy 
        /*if (inputIndex < timeStamps.Count && SongManager.instance.audioSource.isPlaying)
        {
            // Assigned variables for clarity
            double timeStamp = timeStamps[inputIndex];
            double marginOfError = SongManager.instance.errorMargin;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.instance.inputDelay / 1000.0); // milliseconds to seconds

            // If the player hit within the margin of error
            if (Math.Abs(audioTime - timeStamp) < marginOfError)
            {
                // Notify of accurate hit
                print($"Hit on {inputIndex} note");

                // Increment index to next time stamp that needs to be fulfilled
                inputIndex++;
            }
            else
            {
                // Notify of inaccurate hit
                print($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
            }

            // If the player did not hit the object within the margin of error at the specified time stamp
            if (timeStamp + marginOfError <= audioTime)
            {
                // Notify of missed hit
                print($"Missed {inputIndex} note");
                inputIndex++;
            }
        }*/
    }

    public override void SpawnObjects()
    {
        // Time stamps correspond to the number of objects needed to be spawned
        // So, if there if there are still objects to be spawned and if the song is currently playing...
        if (spawnIndex < timeStamps.Count && SongManager.instance.audioSource.isPlaying)
        {
            // This will make it so that the object has sufficient screen time before it can be hit
            // To elaborate, the timestamp is the time at which the object needs to be hit
            // So, to give the player time to react, the timestamp is subtracted by the specified screen time for the object
            // If the current song time is greater than or equal to that...
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.instance.noteScreenTime)
            {
                // Debug.Log("Spawn Index: " + spawnIndex + "\n");

                // Spawn object at timestamp - screentime
                GameObject note = Resources.Load("Prefabs/" + enemyType, typeof(GameObject)) as GameObject;
                var enemy = Instantiate(note, new Vector3(transform.localPosition.x, transform.localPosition.y, SongManager.instance.noteSpawnZ), note.transform.rotation, transform);
                Debug.Log("Enemy Spawned");

                // Add spawned enemy to list
                enemies.Add(enemy.GetComponent<Enemy>());

                // Enemy will know where to position itself so the player can hit
                enemy.GetComponent<Enemy>().assignedTime = (float)timeStamps[spawnIndex];

                // Debug.Log("Assigned Time: " + timeStamps[spawnIndex] + "\n");

                // Move on to next enemy to be spawned
                spawnIndex++;
            }
        }
    }

    public override void RefreshLane()
    {
        // Clear lane to get rid of visual anomalies
        enemies.Clear();
        timeStamps.Clear();

        foreach (Transform child in transform) Destroy(child.gameObject);
    }
}