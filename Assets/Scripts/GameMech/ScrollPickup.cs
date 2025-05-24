using UnityEngine;

public class ScrollPickup : MonoBehaviour
{
    public ParticleSystem pickupEffect;
    private bool pickedUp = false;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private AudioSource bookPickup;

    void Update()
    {

        transform.Rotate(0, rotationSpeed, 0);
    }
    void OnEnable()
    {
        if (GameManager.Instance.HasScroll())
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (pickedUp) return;
        if (other.CompareTag("Player"))
        {
            pickedUp = true;

            // Optional: Play effect
            bookPickup.Play();
            this.GetComponent<MeshRenderer>().enabled = false;
            ParticleSystem effect = Instantiate(pickupEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration);

            // Notify GameManager
            GameManager.Instance.ScrollCollected();

            // Destroy the scroll
            Destroy(gameObject, bookPickup.clip.length);
        }
    }
}
