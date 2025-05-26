using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class WizardInteraction : MonoBehaviour
{
    public GameObject pressEText;
    public Animator playerAnimator;
    public Animator wizardAnimator;
    public GameObject wizardBook;
    public float interactionDuration = 3f;

    private bool playerInRange = false;
    private bool hasInteracted = false;
    private GameObject player;

    void Start()
    {
        pressEText?.SetActive(false);
        wizardBook?.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && !hasInteracted && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interaction started");
            hasInteracted = true;
            pressEText?.SetActive(false);
            StartCoroutine(DeliverBookRoutine());
        }
    }

    IEnumerator DeliverBookRoutine()
    {
        if (player != null)
        {
            // Disable player movement
            player.GetComponent<PlayerMovement>().enabled = false;

            // Rotate player to face wizard
            Vector3 directionToWizard = (transform.position - player.transform.position).normalized;
            directionToWizard.y = 0; // Keep rotation only on Y axis
            Quaternion targetRotation = Quaternion.LookRotation(directionToWizard);
            float elapsed = 0f;

            while (elapsed < 0.5f)
            {
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, elapsed / 0.5f);
                elapsed += Time.deltaTime;
                yield return null;
            }

            player.transform.rotation = targetRotation;
        }

        // Trigger animations
        playerAnimator?.SetTrigger("GiveBook");
        wizardAnimator?.SetTrigger("ReceiveBook");

        yield return new WaitForSeconds(interactionDuration);

        // Show book
        wizardBook?.SetActive(true);
        GameManager.Instance.Book.enabled = false;
        GameManager.Instance.Key.enabled = false;
        if (player != null)
            player.GetComponent<PlayerMovement>().enabled = true;

        GameManager.Instance?.MarkBookDelivered();
        yield return new WaitForSeconds(1.5f);
        if (FadeManager.Instance != null)
            yield return FadeManager.Instance.FadeOut();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("ScoreScene");

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.HasScroll())
        {
            Debug.Log("Player in range");
            player = other.gameObject;
            pressEText?.SetActive(true);
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pressEText?.SetActive(false);
            playerInRange = false;
        }
    }
}
