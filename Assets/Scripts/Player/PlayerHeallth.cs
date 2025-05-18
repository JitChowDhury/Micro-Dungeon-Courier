using UnityEngine;

public class PlayerHeallth : MonoBehaviour
{
    [SerializeField] private float health;

    void Update()
    {
        if (health <= 0)
        {
            Debug.Log("Player is Dead");

        }

    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
    }

}
