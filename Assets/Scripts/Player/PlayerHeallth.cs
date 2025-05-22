using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private CharacterController controller;

    public Transform respawnPoint;

    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
        controller = GetComponent<CharacterController>();
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void Die()
    {
        if (!isDead)
        {
            isDead = true;
            StartCoroutine(RespawnSequence());
        }
    }

    private IEnumerator RespawnSequence()
    {
        yield return FadeManager.Instance.FadeOut();

        if (controller != null) controller.enabled = false; 

        transform.position = respawnPoint.position;

        if (controller != null) controller.enabled = true;  

        currentHealth = maxHealth;
        isDead = false;

        yield return new WaitForSeconds(0.5f);
        yield return FadeManager.Instance.FadeIn();
    }
}
