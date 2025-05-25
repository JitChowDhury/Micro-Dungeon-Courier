using UnityEngine;

public class CoinPile : MonoBehaviour
{
    [SerializeField] private int minValue = 10;
    [SerializeField] private int maxValue = 20;
    [SerializeField] private AudioSource pickupSound; // Optional

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        int value = Random.Range(minValue, maxValue + 1);


        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(value);
        }

        // Optional: play sound
        if (pickupSound != null)
        {
            pickupSound.Play();
            // Delay destroy so sound can finish
            this.GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject, pickupSound.clip.length);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
