using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool hasScroll = false;
    private bool hasKey = false;

    void Awake()
    {
        Instance = this;
    }

    public void ScrollCollected()
    {
        hasScroll = true;
        Debug.Log("Scroll collected!");
    }
    public void KeyCollected()
    {
        hasKey = true;
        Debug.Log("Key Collected");
    }

    public bool HasScroll()
    {
        return hasScroll;
    }
    public bool HasKey()
    {
        return hasKey;
    }

    public void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // TODO: Add level transition, UI, or restart logic
    }
}
