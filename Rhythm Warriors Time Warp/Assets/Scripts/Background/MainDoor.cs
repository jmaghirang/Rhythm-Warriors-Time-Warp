using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDoor : Door
{
    // Start is called before the first frame update
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IntroSequence.destinationReached = true;
        }
    }
}
