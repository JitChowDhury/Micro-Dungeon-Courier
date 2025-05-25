using System.Collections;
using UnityEngine;

public class TorchManager : MonoBehaviour
{
    public static TorchManager Instance;

    public int totalTorches = 3;
    private int torchesLit = 0;
    private Animator anim;

    public GameObject bridgeToActivate;
    [SerializeField] private AudioSource bridgeSound;
    [SerializeField] private AnimationClip bridgeSoundClip;

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
            anim = bridgeToActivate.GetComponent<Animator>();
            anim.SetBool("IsActive", true);
            bridgeSound.Play();
            StartCoroutine(StopBridgeSoundAfter(bridgeSoundClip.length));
        }
    }
    private IEnumerator StopBridgeSoundAfter(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (bridgeSound != null)
        {
            bridgeSound.Stop();
        }
    }
}
