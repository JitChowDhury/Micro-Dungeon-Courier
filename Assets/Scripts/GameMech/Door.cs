using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    private bool isPlayerNear = false;
    private bool hasOpened = false;
    [SerializeField] private GameObject popUpScreen;
    [SerializeField] private AudioSource doorOpen;
    [SerializeField] private AudioSource alert;
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }


    void Update()
    {
        if (hasOpened) return;
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (GameManager.Instance.HasKey())
            {
                animator.SetTrigger("Open");
                doorOpen.Play();
            }
            else
            {
                alert.Play();
                popUpScreen.GetComponentInChildren<TextMeshProUGUI>().text = "Find the key to open door";
                popUpScreen.SetActive(true);

            }
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        popUpScreen.SetActive(false);
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }

    }
}
