using TMPro;
using UnityEngine;
using System.Collections;

public class SceneTitleManager : MonoBehaviour
{
    public TextMeshProUGUI sceneTitleText;
    public CanvasGroup canvasGroup;
    public string title = "Sanctum of Trials";
    public float fadeDuration = 1.5f;
    public float showDuration = 3f;

    private void Start()
    {
        sceneTitleText.text = title;
        StartCoroutine(ShowSceneTitle());
    }

    private IEnumerator ShowSceneTitle()
    {
        // Fade in
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 1;

        // Stay on screen
        yield return new WaitForSeconds(showDuration);

        // Fade out
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0;
    }
}
