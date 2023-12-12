using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Object
{
    public int damageAmount;
    public bool instanceOfDmg = false;

    // Start is called before the first frame update
    void Start()
    {
        timeInstantiated = SongManager.GetAudioSourceTime();
        GetComponentInChildren<MeshRenderer>().enabled = false;

        //player = FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;

        // note time is time between where the note spawns and where it is supposed to be hit; we also need the time between when it's supposed to be hit and when the object is supposed to despawn - hence the *2
        // t = 0 is the spawn location and t = 1 is the despawn location
        float t = (float)(timeSinceInstantiated / (SongManager.instance.noteScreenTime * 2));

        if (t > 1)
        {
            // If the object passes where it is supposed to, then despawn
            Destroy(gameObject);

        }
        else
        {
            // Move along two points - from spawn point to despawn point
            transform.localPosition = Vector3.Lerp(Vector3.forward * SongManager.instance.noteSpawnZ, Vector3.forward * SongManager.instance.noteDespawnZ, t);
            GetComponentInChildren<MeshRenderer>().enabled = true;
        }
    }

    // Check collisions with hit box
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var p = other.GetComponent<Player>();
            if (p.currentHealth != 0)
            {
                if (!instanceOfDmg)
                {
                    p.TakeDamage(damageAmount);
                    Debug.Log("Current health: " + p.currentHealth);
                    Debug.Log("Player hit once");

                    instanceOfDmg = true;
                }

                AudioManager.instance.missSFX.Play();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            instanceOfDmg = false;
        }
    }

    /*
    public float obstacleSpeed = 5f; // speed at which the obstacle moves towards the player
    public int damageAmount = 10; // damage amount when the obstacle hits the player

    void Update()
    {
        MoveObstacle();
    }

    void MoveObstacle()
    {
        // move the obstacle towards the user
        transform.Translate(Vector3.back * obstacleSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // attach to the camera
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }

            Destroy(gameObject);
        }
    }
*/
}
