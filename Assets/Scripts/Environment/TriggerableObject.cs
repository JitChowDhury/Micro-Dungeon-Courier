using UnityEngine;

public class TriggerableObject : MonoBehaviour
{
    public Animator anim;

    public void Activate()
    {
        anim.SetBool("IsOpen", true);
    }

    public void Deactivate()
    {
        anim.SetBool("IsOpen", false);
    }
}
