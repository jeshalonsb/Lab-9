using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TargetSpawner targetSpawner;
    public PlayerShoot playerShoot;

    public float gameDuration = 60f;

    public TMP_Text timerText;
    public TMP_Text gameOverText;

    private float timeRemaining;
    private bool gameActive;

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (!gameActive) return;

        timeRemaining -= Time.deltaTime;
        timeRemaining = Mathf.Max(timeRemaining, 0f);

        timerText.text = "Time: " + Mathf.CeilToInt(timeRemaining);

        if (timeRemaining <= 0f)
        {
            EndGame();
        }
    }

    private void StartGame()
    {
        timeRemaining = gameDuration;
        gameActive = true;

        targetSpawner.enabled = true;
        playerShoot.enabled = true;

        gameOverText.gameObject.SetActive(false);
    }

    private void EndGame()
    {
        gameActive = false;

        targetSpawner.enabled = false;
        playerShoot.enabled = false;

        gameOverText.text = "Time's Up!";
        gameOverText.gameObject.SetActive(true);
    }
}