using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLane : Lane
{
    public List<Obstacle> obstacles = new();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnObjects();
    }

    public override void SpawnObjects()
    {
        if (spawnIndex < timeStamps.Count && SongManager.instance.audioSource.isPlaying)
        {
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.instance.noteScreenTime)
            {
                // Debug.Log("Spawn Index: " + spawnIndex + "\n");

                // Spawn enemy
                GameObject note = Instantiate(Resources.Load("Prefabs/Obstacle", typeof(GameObject)), transform) as GameObject;
                Debug.Log("Obstacle Spawned");

                // Add spawned enemy to list
                obstacles.Add(note.GetComponent<Obstacle>());

                // Enemy will know where to position itself so the player can hit
                note.GetComponent<Obstacle>().assignedTime = (float)timeStamps[spawnIndex];
                // Debug.Log("Assigned Time: " + timeStamps[spawnIndex] + "\n");

                // Move on to next enemy to be spawned
                spawnIndex++;
            }
        }
    }

    public override void RefreshLane()
    {
        obstacles.Clear();
        timeStamps.Clear();

        foreach (Transform child in transform) Destroy(child.gameObject);
    }
}
