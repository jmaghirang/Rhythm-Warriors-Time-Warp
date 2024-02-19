using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    protected double timeInstantiated;
    public float assignedTime;

    public virtual void OnTriggerEnter(Collider other)
    {

    }
}