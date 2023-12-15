using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLane : Lane
{
    // List of obstacles in a scene
    public List<Obstacle> obstacles = new();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn obstacles
        SpawnObjects();
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

                // Spawn obstacle at timestamp - screentime
                GameObject note = Resources.Load("Prefabs/Obstacle", typeof(GameObject)) as GameObject;
                var obs = Instantiate(note, transform);
                Debug.Log("Obstacle Spawned");

                // Add spawned obstacle to list
                obstacles.Add(obs.GetComponent<Obstacle>());

                // Obstacle will know where to position itself so the player can hit
                obs.GetComponent<Obstacle>().assignedTime = (float)timeStamps[spawnIndex];

                // Debug.Log("Assigned Time: " + timeStamps[spawnIndex] + "\n");

                // Move on to next enemy to be spawned
                spawnIndex++;
            }
        }
    }

    public override void RefreshLane()
    {
        // Clear lane to get rid of visual anomalies
        obstacles.Clear();
        timeStamps.Clear();

        foreach (Transform child in transform) Destroy(child.gameObject);
    }
}
