using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public ParticleSystem pickupEffect;
    private bool pickedUp = false;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private AudioSource keyPickup;


    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);
    }
    void OnTriggerEnter(Collider other)
    {
        if (pickedUp) return;
        if (other.CompareTag("Player"))
        {
            pickedUp = true;

            keyPickup.Play();
            this.GetComponent<MeshRenderer>().enabled = false;
            ParticleSystem effect = Instantiate(pickupEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration);

            // Notify GameManager
            GameManager.Instance.KeyCollected();


            Destroy(gameObject, keyPickup.clip.length);
        }
    }
}

