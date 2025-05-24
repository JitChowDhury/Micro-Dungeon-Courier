using TMPro;
using UnityEngine;

public class PortalExit : MonoBehaviour
{
    [SerializeField] private GameObject popUpScreen;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.HasScroll())
            {
                GameManager.Instance.CompleteLevel();
            }
            else
            {
                popUpScreen.GetComponentInChildren<TextMeshProUGUI>().text = "Take the book to exit";
                popUpScreen.SetActive(true);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        popUpScreen.SetActive(false);
    }
}
