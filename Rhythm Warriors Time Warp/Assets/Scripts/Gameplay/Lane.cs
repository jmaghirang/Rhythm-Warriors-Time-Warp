using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

// Code referenced from
// https://youtu.be/ev0HsmgLScg?si=r4zVEzDwRoTi50C9

public class Lane : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRepresentation; // Note from midi file to translate to a lane

    public GameObject enemyPrefab; 
    public GameObject obstaclePrefab;

    public List<Enemy> enemies = new();
    public List<double> timeStamps = new(); // The times at which the player needs to hit an enemy
    public List<double> obstacleTimeStamps = new(); // Timestamps for obstacle spawn
    
    int spawnIndex = 0; // Index of current enemy that spawns
    // int timeIndex = 0;

    int obstacleSpawnIndex = 0; // Index of current obstacle to spawn

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemies();

        if (obstacleSpawnIndex < obstacleTimeStamps.Count && SongManager.instance.audioSource.isPlaying)
        {
            if (SongManager.GetAudioSourceTime() >= obstacleTimeStamps[obstacleSpawnIndex] - SongManager.instance.noteScreenTime)
            {
                // Spawn obstacle
                var obstacle = Instantiate(obstaclePrefab, transform);
                // Additional settings for obstacles, if needed

                obstacleSpawnIndex++;
            }
        }

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

    public void SpawnEnemies()
    {
        if (spawnIndex < timeStamps.Count && SongManager.instance.audioSource.isPlaying)
        {
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.instance.noteScreenTime)
            {
                // Debug.Log("Spawn Index: " + spawnIndex + "\n");

                // Spawn enemy
                var note = Instantiate(enemyPrefab, transform);
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

    public void SetTimeStamps(Note[] array)
    {
        foreach (var enemy in array)
        {
            // If the note name of the enemy (which will be child to a Lane object) is equal to the note specified in the inspector
            if (enemy.NoteName == noteRepresentation)
            {
                // Convert time from midi file tempo map to metric time
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(enemy.Time, SongManager.midiFile.GetTempoMap());
                // Convert time span to seconds + milliseconds
                timeStamps.Add((double) metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double) metricTimeSpan.Milliseconds / 1000f);
            }
        }
    }

    public void SetObstacleTimeStamps(List<double> timestamps)
    {
        obstacleTimeStamps = timestamps;
    }


    public void RefreshLane()
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