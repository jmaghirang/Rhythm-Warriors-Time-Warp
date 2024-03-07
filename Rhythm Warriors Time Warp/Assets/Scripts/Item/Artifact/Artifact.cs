using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Jamie Lee

public class Artifact : MonoBehaviour
{
    public static Artifact instance;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField]
    public Dictionary<int, Fragment> fragments = new()
    {
        { 1, new Fragment {ID=1, isCollected=false} },
        { 2, new Fragment {ID=2, isCollected=false} },
        { 3, new Fragment {ID=3, isCollected=false} },
        { 4, new Fragment {ID=4, isCollected=false} },
        { 5, new Fragment {ID=5, isCollected=false} }
    };
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
