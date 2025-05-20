using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light torchLight;
    public float minIntensity = 1f;
    public float maxIntensity = 4.5f;
    public float flickerSpeed = 0.1f;

    void Start()
    {
        if (torchLight == null)
            torchLight = GetComponent<Light>();

        InvokeRepeating("Flicker", 0f, flickerSpeed);
    }

    void Flicker()
    {
        torchLight.intensity = Random.Range(minIntensity, maxIntensity);
    }
}
