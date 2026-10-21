using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;

    private int score;

    private void OnEnable()
    {
        Target.OnTargetHit += AddScore;
    }
    private void OnDisable()
    {
        Target.OnTargetHit -= AddScore;
    }
    private void Start()
    {
        UpdateScore();
    }
    private void AddScore (int points)
    {
        score += points;
        UpdateScore();
    }
    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}
