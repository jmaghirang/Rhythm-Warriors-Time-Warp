using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int bpm;

    public string enemyName;
    public bool canBeHit;

    // Start is called before the first frame update
    void Start()
    {
        bpm = AudioManager.instance.currentMusic.BPM;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.startPlaying)
        {
            transform.position -= new Vector3(0, 0, bpm * Time.deltaTime);
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Slice")
        {
            canBeHit = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (gameObject.activeInHierarchy)
        {
            if (other.tag == "Slice")
            {
                canBeHit = false;
            }
        }
    }


}
