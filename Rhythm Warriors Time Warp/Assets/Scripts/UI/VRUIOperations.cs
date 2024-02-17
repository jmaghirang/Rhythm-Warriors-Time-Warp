using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// some code referenced from: 
// https://www.youtube.com/watch?v=wVD4BKILJDs

public class VRUIOperations : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    private void OnTriggerEnter(Collider other)
    {
        TrigExit.instance.currentCollider = GetComponent<VRUIOperations>();
        OnEnter.Invoke();
    }
}
