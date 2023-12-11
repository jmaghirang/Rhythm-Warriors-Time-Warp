using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
    }

    public AudioSource bgMusic; // Background/Main music if any

    // Win and lose scenario music
    public AudioSource winMusic;
    public AudioSource loseMusic;

    // Hit and miss SFX
    public AudioSource hitSFX;
    public AudioSource missSFX;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
