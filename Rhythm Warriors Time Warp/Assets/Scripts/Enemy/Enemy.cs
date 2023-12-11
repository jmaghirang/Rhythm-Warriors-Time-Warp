using UnityEngine;

// Code referenced from
// https://youtu.be/ev0HsmgLScg?si=r4zVEzDwRoTi50C9

public class Enemy : MonoBehaviour
{
    public bool canBeHit;

    double timeInstantiated;
    public float assignedTime; // time the player is supposed to hit it

    // Start is called before the first frame update
    void Start()
    {
        timeInstantiated = SongManager.GetAudioSourceTime();
    }

    // Update is called once per frame
    void Update()
    {
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;

        // note time is time between where the note spawns and where it is supposed to be hit; we also need the time between when it's supposed to be hit and when the object is supposed to despawn - hence the *2
        // t = 0 is the spawn location and t = 1 is the despawn location
        float t = (float) (timeSinceInstantiated / (SongManager.instance.noteScreenTime * 2));
       
        if (t > 1)
        {
            // If the object passes where it is supposed to despawn
            Destroy(gameObject);
        }
        else
        {
            // Move along two points - from spawn point to despawn point
            transform.localPosition = Vector3.Lerp(Vector3.forward * SongManager.instance.noteSpawnZ, Vector3.forward * SongManager.instance.noteDespawnZ, t);
        }
    }

    // Check collisions with hit box
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Slice"))
        {
            canBeHit = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (gameObject.activeInHierarchy)
        {
            if (other.CompareTag("Slice"))
            {
                canBeHit = false;
            }
        }
    }
}
