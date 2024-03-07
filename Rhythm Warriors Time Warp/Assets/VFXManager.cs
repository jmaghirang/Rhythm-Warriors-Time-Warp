using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXManager : MonoBehaviour
{
    public static VFXManager instance;

    private void Awake()
    {
        instance = this;

    }
    public List<ParticleSystem> effects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableEffects()
    {
        foreach(var effect in effects)
        {
            effect.Clear();
            effect.gameObject.SetActive(false);
        }
    }

    public void EnableEffects()
    {
        foreach (var effect in effects)
        {
            effect.Play();
            effect.gameObject.SetActive(true);
        }
    }
}
