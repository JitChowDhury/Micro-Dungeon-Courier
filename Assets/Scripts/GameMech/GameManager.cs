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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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
    public IEnumerator PlayerDied(System.Action<bool> onComplete)
    {
        deathCount++;
        UpdateDeathCountText();

        if (deathCount >= 3)
        {
            yield return FadeManager.Instance.FadeOut();
            hasKey = false;
            hasScroll = false;
            deathCount = 0;


            SceneManager.LoadScene(0);
            yield return new WaitForSeconds(0.5f);
            yield return FadeManager.Instance.FadeIn();

            onComplete?.Invoke(true);
        }
        else
        {
            onComplete?.Invoke(false); 
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Try to find the new text reference in the new scene
        deathcount = GameObject.Find("DeathCount")?.GetComponent<TextMeshProUGUI>();
        UpdateDeathCountText();
    }

    private void UpdateDeathCountText()
    {
        if (deathcount != null)
            deathcount.text = (3 - deathCount).ToString();
    }

}
