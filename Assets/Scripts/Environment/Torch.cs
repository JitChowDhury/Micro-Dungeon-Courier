using UnityEngine;

public class Torch : MonoBehaviour
{
    public bool isLit = false;
    public ParticleSystem fireEffect;
    public Light fireLight;

    public void LightTorch()
    {
        if (isLit) return;

        isLit = true;
        if (fireEffect != null) fireEffect.Play();
        if (fireLight != null) fireLight.enabled = true;

        TorchManager.Instance.TorchLit();
    }

    private void Start()
    {
        if (fireLight != null) fireLight.enabled = false;
        if (fireEffect != null) fireEffect.Stop();
    }
}
