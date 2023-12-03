using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Audio : MonoBehaviour
{
    // Attributes of audio defined
    public AudioSource music;
    public int BPM;
    public float volume; 

    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        volume = music.volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
