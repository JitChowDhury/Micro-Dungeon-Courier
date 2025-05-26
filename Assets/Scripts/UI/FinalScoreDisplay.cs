using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class FinalScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    

    void Start()
    {
        // Fetch score from GameManager
        int finalScore = ScoreManager.Instance.GetScore();

        scoreText.text = "Score: " + finalScore.ToString();





    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);

    }
}
