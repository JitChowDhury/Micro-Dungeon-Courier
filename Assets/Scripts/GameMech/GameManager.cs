using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool hasScroll = false;
    private bool hasKey = false;

    void Awake()
    {
        // If an instance already exists and it's not this one, destroy this object
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Otherwise, make this the instance and persist across scenes
        Instance = this;
        DontDestroyOnLoad(gameObject);
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
    }
}
