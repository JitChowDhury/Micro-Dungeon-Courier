using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroManager : MonoBehaviour
{
    public GameObject introPanel;
    public TextMeshProUGUI introText;
    public float typingSpeed = 0.03f;
    public AudioSource blipSound; // Assign a typing sound (small click/blip) in Inspector
    public string nextSceneName = "Level1"; // Your gameplay scene name

    private string[] storyLines = new string[]
    {
        "In a world of miniature wonders...",
        "A powerful magical book has been stolen.",
        "You are the chosen one.\nDeliver the book to the wizard before it's too late!"
    };

    private bool isSkipping = false;

    void Start()
    {
        StartCoroutine(PlayIntro());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSkipping = true;
            SceneManager.LoadScene(nextSceneName);
        }
    }

    IEnumerator PlayIntro()
    {
        introPanel.SetActive(true);

        foreach (string line in storyLines)
        {
            yield return StartCoroutine(TypeLine(line));
            yield return new WaitForSeconds(1.5f);

            if (isSkipping) yield break;
        }

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator TypeLine(string line)
    {
        introText.text = "";
        foreach (char letter in line.ToCharArray())
        {
            if (isSkipping) yield break;

            introText.text += letter;

            // Play typing sound for non-space characters
            if (!char.IsWhiteSpace(letter) && blipSound != null)
                blipSound.Play();

            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
