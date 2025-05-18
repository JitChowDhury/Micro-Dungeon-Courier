using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    private bool isPlayerNear = false;
    private bool hasOpened = false;
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
            }
            else
            {
                Debug.Log("You need the key first!");
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
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
