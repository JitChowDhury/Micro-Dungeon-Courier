using UnityEngine;

public class TriggerableObject : MonoBehaviour
{
    public Animator anim;
    [SerializeField] private AudioSource doorSound;

    public void Activate()
    {
        anim.SetBool("IsOpen", true);
        doorSound.Play();

    }

    public void Deactivate()
    {
        anim.SetBool("IsOpen", false);
    }
}
