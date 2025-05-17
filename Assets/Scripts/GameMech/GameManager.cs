using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool hasScroll = false;

    void Awake()
    {
        Instance = this;
    }

    public void ScrollCollected()
    {
        hasScroll = true;
        Debug.Log("Scroll collected!");
    }

    public bool HasScroll()
    {
        return hasScroll;
    }

    public void CompleteLevel()
    {
        Debug.Log("Level Complete!");
        // TODO: Add level transition, UI, or restart logic
    }
}
