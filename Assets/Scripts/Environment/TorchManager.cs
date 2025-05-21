using UnityEngine;

public class TorchManager : MonoBehaviour
{
    public static TorchManager Instance;

    public int totalTorches = 3;
    private int torchesLit = 0;

    public GameObject bridgeToActivate;

    private void Awake()
    {
        Instance = this;
    }

    public void TorchLit()
    {
        torchesLit++;

        if (torchesLit >= totalTorches)
        {
            Debug.Log("All torches lit! Activate bridge.");
            bridgeToActivate.SetActive(true); // or animate it
        }
    }
}
