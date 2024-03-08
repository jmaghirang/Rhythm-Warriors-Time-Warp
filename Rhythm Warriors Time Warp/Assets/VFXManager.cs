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

    public void TriggerVFX(Enemy enemy)
    {
        StartCoroutine(TriggerEffectsRoutine(enemy));
    }

    public IEnumerator TriggerEffectsRoutine(Enemy enemy)
    {
        GameObject hitFX = Instantiate(enemy.vfx, Vector3.up + enemy.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(1f);

        Destroy(hitFX);
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
