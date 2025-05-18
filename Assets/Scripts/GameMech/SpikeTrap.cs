using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public float damage = 20f;
    public float triggerInterval = 5f;
    public Animator spikeAnimator; // Assign in Inspector

    private bool isActive = false;

    private void Start()
    {
        InvokeRepeating(nameof(TriggerSpike), 0f, triggerInterval);
    }

    private void TriggerSpike()
    {
        isActive = true;
        spikeAnimator.SetTrigger("Activate"); // You need an "Activate" trigger in Animator
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;

        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = false; // Reset after spike finishes cycle
        }
    }
}
