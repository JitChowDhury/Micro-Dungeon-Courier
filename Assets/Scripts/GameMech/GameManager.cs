using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool hasScroll = false;
    private bool hasKey = false;
    private int deathCount = 0;
    private HashSet<string> collectedCoins = new HashSet<string>();
    private bool bookDelivered = false;
    [SerializeField] private TextMeshProUGUI deathcount;
    [SerializeField] public Image Book;
    [SerializeField] public Image Key;

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
        Book.enabled = true;
        Debug.Log("Scroll collected!");
    }

    public void KeyCollected()
    {
        hasKey = true;
        Key.enabled = true;
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
            Book.enabled = false;
            Key.enabled = false;
            ScoreManager.Instance.ResetScore();

            deathCount = 0;

            collectedCoins.Clear();
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
    public void CollectCoin(string id)
    {
        collectedCoins.Add(id);
    }

    public bool IsCoinCollected(string id)
    {
        return collectedCoins.Contains(id);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Try to find the new text reference in the new scene
        deathcount = GameObject.Find("DeathCount")?.GetComponent<TextMeshProUGUI>();
        Book = GameObject.Find("BookImage")?.GetComponent<Image>();
        Key = GameObject.Find("KeyImage")?.GetComponent<Image>();
        UpdateDeathCountText();
        if (hasScroll && Book != null) Book.enabled = true;
        if (hasKey && Key != null) Key.enabled = true;
    }

    private void UpdateDeathCountText()
    {
        if (deathcount != null)
            deathcount.text = (3 - deathCount).ToString();

    }
    public void MarkBookDelivered()
    {
        bookDelivered = true;
        hasScroll = false;
    }



}
