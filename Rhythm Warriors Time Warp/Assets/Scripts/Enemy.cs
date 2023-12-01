using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public bool canBeHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
