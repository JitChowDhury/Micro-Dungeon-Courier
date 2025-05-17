using UnityEngine;

public class ScrollPickup : MonoBehaviour
{
    //public GameObject pickupEffect;
    private bool pickedUp = false;

    void OnTriggerEnter(Collider other)
    {
        if (pickedUp) return;
        if (other.CompareTag("Player"))
        {
            pickedUp = true;

            // Optional: Play effect
            //if (pickupEffect) Instantiate(pickupEffect, transform.position, Quaternion.identity);

            // Notify GameManager
            GameManager.Instance.ScrollCollected();

            // Destroy the scroll
            Destroy(gameObject);
        }
    }
}
