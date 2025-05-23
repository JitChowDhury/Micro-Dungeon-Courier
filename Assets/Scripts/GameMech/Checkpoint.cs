using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform CPRespawnPoint;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().respawnPoint = CPRespawnPoint;
        }
    }
}
