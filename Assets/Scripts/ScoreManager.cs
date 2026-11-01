using UnityEngine;
using TMPro;
using Unity.Mathematics;

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
    public int GetScore()
    {
        return score;
    }

    public void SetScore(int newScore)
    {
        score = newScore;
        UpdateScore();
    }
}
