using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// source: http://www.zulubo.com/gamedev/2019/1/5/vr-screen-shake-the-art-of-not-throwing-up

[ExecuteInEditMode]
public class ScreenShakeVR : MonoBehaviour
{
    // singleton 
    // so it can be globally accessible, useful for later when we add more levels
    public static ScreenShakeVR instance;

    // material for screen shake effect
    Material material;

    // parameters controlling the screen shake
    public float shakeMagnitude = 0.1f;
    public float shakeFrequency = 20f;

    // variables to store current shake values & cumulative shake strength
    private float shakeVal;
    float shakeCumulation;

    // test/debug if it works using space bar
    [Tooltip("Shake the screen when the space key is pressed")]
    public bool debug = false;

    // magnitude, length, and exponent
    public class ShakeEvent
    {
        public float magnitude;
        public float length;

        private float exponent;

        public bool finished { get { return time >= length; } }
        public float currentStrength { get { return magnitude * Mathf.Clamp01(1 - time / length); } }

        // initialize the shake event
        public ShakeEvent(float mag, float len, float exp = 2)
        {
            magnitude = mag;
            length = len;
            exponent = exp;
        }

        private float time;

        // track the time progress of the shake event
        public void Update(float deltaTime)
        {
            time += deltaTime;
        }
    }

    // list to store active shake events
    public List<ShakeEvent> activeShakes = new List<ShakeEvent>();

    // private material used for the effect
    void Awake()
    {
        // instance to the current script for singleton access
        instance = this;

        // check if the material is already assigned
        if (material != null)
        {
            // if yes, set the shader for the material
            material.shader = Shader.Find("Hidden/ScreenShakeVR");
        }
        else
        {
            // if no material, create a new one and set its shader
            material = new Material(Shader.Find("Hidden/ScreenShakeVR"));
        }
    }

    // make sure the script is enabled and Awake is called
    private void OnEnable()
    {
        Awake();
    }


    /// trigger a shake event
    /// <param name="magnitude">Magnitude of the shaking. Should range from 0 - 1</param>
    /// <param name="length">Length of the shake event.</param>
    /// <param name="exponent">Falloff curve of the shaking</param>
    public void Shake(float magnitude, float length, float exponent = 2)
    {
        // add a new shake event to the active shakes list
        activeShakes.Add(new ShakeEvent(magnitude, length, exponent));
    }

    /// trigger a global shake event
    /// <param name="magnitude">Magnitude of the shaking. Should range from 0 - 1</param>
    /// <param name="length">Length of the shake event.</param>
    /// <param name="exponent">Falloff curve of the shaking</param>
    public static void TriggerShake(float magnitude, float length, float exponent = 2)
    {
        // check if there is an instance of the script
        if (instance == null)
        {
            // log a warning if no instance is found
            Debug.LogWarning("No ScreenShakeVR Component in scene. Add one to a camera.");
        }
        else
        {
            // trigger a shake event using the instance
            instance.Shake(magnitude, length, exponent);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // check if the space key is pressed and debug mode is enabled
        if (Input.GetKeyDown(KeyCode.Space) && debug)
        {
            // trigger a shake event with predefined values for testing
            Shake(0.5f, 1.0f);
        }

        shakeCumulation = 0;

        // iterate through all active shake events
        for (int i = activeShakes.Count - 1; i >= 0; i--)
        {
            // accumulate their current magnitude
            activeShakes[i].Update(Time.deltaTime);
            shakeCumulation += activeShakes[i].currentStrength;

            // remove shake events that have finished
            if (activeShakes[i].finished)
            {
                activeShakes.RemoveAt(i);
            }
        }

        // calculate the current shake value using perlin noise
        if (shakeCumulation > 0)
        {
            shakeVal = Mathf.PerlinNoise(Time.time * shakeFrequency, 10.234896f) * shakeCumulation * shakeMagnitude;
        }
        
        else
        {
            shakeVal = 0;
        }
    }

    // postprocess the image using the material with the screen shake effect
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        // check if the shake value is approximately not zero
        if (Mathf.Approximately(shakeVal, 0) == false)
        {
            // set the shake value in the material
            material.SetFloat("_ShakeFac", shakeVal);

            // apply the material to the source texture and render to destination
            Graphics.Blit(source, destination, material);
        }

        else
        {
            // if no shaking currently, simply copy the source to destination
            Graphics.Blit(source, destination);
        }
    }
}