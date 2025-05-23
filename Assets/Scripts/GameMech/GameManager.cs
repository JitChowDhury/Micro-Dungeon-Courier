using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool hasScroll = false;
    private bool hasKey = false;
    private int deathCount = 0;
    [SerializeField] private TextMeshProUGUI deathcount;

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
    public IEnumerator PlayerDied()
    {
        deathCount++;
        deathcount.text = (3 - deathCount).ToString(); // Update the UI with the remaining lives.

        if (deathCount >= 3)
        {
            yield return FadeManager.Instance.FadeOut();
            hasKey = false;
            hasScroll = false;
            deathCount = 0;
            deathcount.text = "3"; // Reset the UI for a new game.
            SceneManager.LoadScene(0);
            yield return new WaitForSeconds(0.5f);
            yield return FadeManager.Instance.FadeIn();
        }
    }

}
