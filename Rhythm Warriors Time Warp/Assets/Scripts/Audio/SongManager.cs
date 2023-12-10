using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

// Code referenced from
// https://youtu.be/ev0HsmgLScg?si=r4zVEzDwRoTi50C9

public class SongManager : MonoBehaviour
{
    public static SongManager instance;

    private void Awake()
    {
        instance = this;
    }

    public bool musicPlaying = false;

    public AudioSource audioSource;

    public Lane[] lanes;

    public float songDelay; // seconds - delay from start of song
    public double errorMargin; // seconds - how incorrect player can be

    public int inputDelay; // milliseconds

    public string fileLocation; // midi file location

    public float noteTime; // player reaction time; time object will be on screen
    public float noteSpawnZ; // position where objects will spawn
    public float noteTapZ;  // position where objects will need to be hit
    public float noteDespawnZ // position where objects will be destroyed if not hit
    {
        get
        {
            return noteTapZ - (noteSpawnZ - noteTapZ);
        }
    }

    public static MidiFile midiFile;

    // Start is called before the first frame update
    void Start()
    {
        if (Application.streamingAssetsPath.StartsWith("jar:file//"))
        {
            // If on Android
            StartCoroutine(ReadFromWeb());
        }
        else
        {
            // If on Windows, Mac,...
            ReadFromFile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ReadFromWeb()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(Application.streamingAssetsPath + "/" + fileLocation))
        {
            // Send request to web server and wait for response
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                // Read results from data
                byte[] results = www.downloadHandler.data;
                // Send results to memory stream
                using (var stream = new MemoryStream(results))
                {
                    // Load stream to midi file
                    midiFile = MidiFile.Read(stream);
                    GetMidiData();
                }
            }
        }
    }

    private void ReadFromFile()
    {
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
        GetMidiData();
    }

    public void GetMidiData()
    {
        // Copy notes from midi file to array
        var notes = midiFile.GetNotes();
        var array = new Note[notes.Count];
        notes.CopyTo(array, 0);
        
        // assign notes in array to respective time stamps
        foreach (var lane in lanes) lane.SetTimeStamps(array);

        // Call function after song delay in seconds
        Invoke(nameof(StartSong), songDelay);
    }

    public void StartSong()
    {
        audioSource.Play();
        musicPlaying = true;
        Debug.Log("Song Started at: " + Time.time);
    }

    // Make the time of audio source a double value for preciseness/smoothness
    public static double GetAudioSourceTime()
    {
        return (double) instance.audioSource.timeSamples / instance.audioSource.clip.frequency;
    }
}
