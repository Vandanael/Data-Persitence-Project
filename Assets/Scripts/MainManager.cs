using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    // Existing UI elements
    public Text ScoreText;
    public GameObject GameOverText;

    // New UI elements
    public TextMeshProUGUI PlayerNameText;
    public TextMeshProUGUI HighScoreText;

    // Reference to High Score UI (if you're using it)
    public HighScoreUI highScoreUI;

    private bool m_Started = false;
    private int m_Points;
    private bool m_GameOver = false;

    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        // Display player name and high score
        if (DataManager.Instance != null)
        {
            PlayerNameText.text = "Player: " + DataManager.Instance.PlayerName;
            UpdateHighScoreText();
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();
                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    private void UpdateHighScoreText()
    {
        if (DataManager.Instance != null && DataManager.Instance.HighScores.Count > 0)
        {
            // Get the top score (the first one in the sorted list)
            HighScoreEntry topScore = DataManager.Instance.HighScores[0];
            HighScoreText.text = "Best Score: " + topScore.score +
                             " by " + topScore.playerName;
        }
        else
        {
            HighScoreText.text = "Best Score: 0";
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        // Save score when game is over
        if (DataManager.Instance != null)
        {
            DataManager.Instance.SaveScore(m_Points);

            // Update the high score display
            UpdateHighScoreText();

            // Update the high score UI if it exists
            if (highScoreUI != null)
            {
                highScoreUI.UpdateHighScoreUI();
            }
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0); // Load the Start Menu scene
    }
}