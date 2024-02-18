using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// debugging
public class SequenceManager : MonoBehaviour
{
    public static SequenceManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExecuteRoutine(bool flag, int index, int pIndex, IEnumerator coroutine)
    {
        flag = false;

        if (!flag && index == pIndex)
        {
            StartCoroutine(coroutine);
            flag = true;
        }
    }
}
