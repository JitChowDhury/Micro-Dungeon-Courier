using System.Collections;
using TMPro;
using UnityEngine;

public class RiddleManager : MonoBehaviour
{
    public TextMeshProUGUI riddleText;
    public float displayTime = 4f;

    private void Start()
    {
        // Ensure it's invisible on start
        riddleText.CrossFadeAlpha(0f, 0f, true);
    }
    public void ShowRiddle(string message)
    {
        StopAllCoroutines();
        StartCoroutine(DisplayRiddle(message));
    }

    private IEnumerator DisplayRiddle(string message)
    {
        riddleText.text = message;

        // Fade In
        riddleText.CrossFadeAlpha(1f, 0.5f, false);

        yield return new WaitForSeconds(displayTime);

        // Fade Out
        riddleText.CrossFadeAlpha(0f, 0.5f, false);
    }

}
