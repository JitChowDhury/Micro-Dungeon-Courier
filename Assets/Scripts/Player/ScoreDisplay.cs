using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TMP_Text scoreText;

    void Update()
    {
        scoreText.text = ScoreManager.Instance.GetScore().ToString();
    }
}
