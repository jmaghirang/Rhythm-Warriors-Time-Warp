using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator doorAnimator;

    private void OnTriggerEnter(Collider other)
    {
        AudioSource sfx = GetComponent<AudioSource>();

        if (other.CompareTag("Player"))
        {
            doorAnimator.SetBool("character_nearby", true);
            sfx.Play();
        }
    }
}
