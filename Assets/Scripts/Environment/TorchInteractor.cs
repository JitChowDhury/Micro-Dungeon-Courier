using UnityEngine;

public class TorchInteractor : MonoBehaviour
{
    private Torch nearbyTorch;

    void Update()
    {
        if (nearbyTorch != null && Input.GetKeyDown(KeyCode.E))
        {
            nearbyTorch.LightTorch();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Torch>() != null)
        {
            nearbyTorch = other.GetComponent<Torch>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Torch>() == nearbyTorch)
        {
            nearbyTorch = null;
        }
    }
}
