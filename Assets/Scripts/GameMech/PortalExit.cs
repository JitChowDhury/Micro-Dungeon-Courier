using UnityEngine;

public class PortalExit : MonoBehaviour
{
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
                Debug.Log("You need the scroll first!");
            }
        }
    }
}
