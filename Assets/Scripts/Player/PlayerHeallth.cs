using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private CharacterController controller;
    private Animator animator;
    public Transform respawnPoint;

    public bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
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

            if (animator != null)
            {
                animator.SetTrigger("Die");
            }

            StartCoroutine(GameManager.Instance.PlayerDied((bool resetOccurred) =>
            {
                if (!resetOccurred)
                {
                    StartCoroutine(RespawnSequence());
                }
                // else: skip respawn because the game reset
            }));
        }
    }

    private IEnumerator RespawnSequence()
    {
        yield return new WaitForSeconds(1.5f);//waiting for death to finish

        yield return FadeManager.Instance.FadeOut();
        if (controller != null) controller.enabled = false;


        transform.position = respawnPoint.position;


        currentHealth = maxHealth;
        isDead = false;

        yield return new WaitForSeconds(0.5f);
        if (controller != null) controller.enabled = true;
        animator.SetTrigger("Respawn");
        yield return FadeManager.Instance.FadeIn();
    }
}
