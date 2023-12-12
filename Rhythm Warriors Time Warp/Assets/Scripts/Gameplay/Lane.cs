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

    public GameObject prefab;

    public List<double> timeStamps = new(); // The times at which the player needs to hit an enemy

    protected int spawnIndex = 0; // Index of current enemy that spawns
    // int timeIndex = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void SpawnObjects()
    {

    }

    public virtual void RefreshLane()
    {

    }

    public void SetTimeStamps(Note[] array)
    {
        foreach (var obj in array)
        {
            // If the note name of the enemy (which will be child to a Lane object) is equal to the note specified in the inspector
            if (obj.NoteName == noteRepresentation)
            {
                // Convert time from midi file tempo map to metric time
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(obj.Time, SongManager.midiFile.GetTempoMap());
                // Convert time span to seconds + milliseconds
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
            }
        }
    }
}