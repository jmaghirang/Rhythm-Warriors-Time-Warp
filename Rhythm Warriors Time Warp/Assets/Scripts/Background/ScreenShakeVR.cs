using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class ScreenShakeVR : MonoBehaviour
{
    [SerializeField]
    private Material material;

    // singleton
    [SerializeField]
    private static ScreenShakeVR _instance;

    public static ScreenShakeVR Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ScreenShakeVR>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("ScreenShakeVR");
                    _instance = obj.AddComponent<ScreenShakeVR>();
                }
            }
            return _instance;
        }
    }

    // parameters controlling the screen shake
    public float shakeMagnitude = 0.1f;
    public float shakeFrequency = 20f;

    // variables to store current shake values & cumulative shake strength
    private float shakeVal;
    private float shakeCumulation;

    [Tooltip("Shake the screen when the space key is pressed")]
    public bool debug = false;

    // magnitude, length, and exponent
    public class ShakeEvent
    {
        public float magnitude;
        public float length;

        private float exponent;
        private float time;

        public bool finished { get { return time >= length; } }
        public float currentStrength { get { return magnitude * Mathf.Clamp01(1 - time / length); } }

        // initialize the shake event
        public ShakeEvent(float mag, float len, float exp = 2)
        {
            magnitude = mag;
            length = len;
            exponent = exp;
        }

        // track the time progress of the shake event
        public void Update(float deltaTime)
        {
            time += deltaTime;
        }
    }

    // list to store active shake events
    public List<ShakeEvent> activeShakes = new List<ShakeEvent>();

    void Awake()
    {
        if (material == null)
        {
            material = new Material(Shader.Find("Hidden/ScreenShakeVR"));
        }
    }

    private void OnEnable()
    {
        Awake();
    }

    public void Shake(float magnitude, float length, float exponent = 2)
    {
        activeShakes.Add(new ShakeEvent(magnitude, length, exponent));
    }

    public static void TriggerShake(float magnitude, float length, float exponent = 2)
    {
        if (Instance == null)
        {
            Debug.LogWarning("No ScreenShakeVR Component in scene. Add one to a camera.");
        }
        else
        {
            Instance.Shake(magnitude, length, exponent);
        }
    }

    private void CheckDebugInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && debug)
        {
            Shake(0.5f, 1.0f);
        }
    }

    private void UpdateActiveShakes()
    {
        shakeCumulation = 0;

        for (int i = activeShakes.Count - 1; i >= 0; i--)
        {
            activeShakes[i].Update(Time.deltaTime);
            shakeCumulation += activeShakes[i].currentStrength;

            if (activeShakes[i].finished)
            {
                activeShakes.RemoveAt(i);
            }
        }
    }

    private void CalculateShakeValue()
    {
        shakeVal = shakeCumulation > 0 ?
            Mathf.PerlinNoise(Time.time * shakeFrequency, 10.234896f) * shakeCumulation * shakeMagnitude :
            0;
    }

    private void Update()
    {
        CheckDebugInput();
        UpdateActiveShakes();
        CalculateShakeValue();
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (Mathf.Approximately(shakeVal, 0) == false)
        {
            material.SetFloat("_ShakeFac", shakeVal);
            Graphics.Blit(source, destination, material);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}