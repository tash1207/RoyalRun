using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

    int score = 0;

    public void IncreaseScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }
}
