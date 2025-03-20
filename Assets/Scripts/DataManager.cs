using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class HighScoreEntry
{
    public string playerName;
    public int score;
    public HighScoreEntry(string name, int score)
    {
        this.playerName = name;
        this.score = score;
    }
}

[Serializable]
public class HighScoreList
{
    public List<HighScoreEntry> highScores = new List<HighScoreEntry>();
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public string PlayerName;
    public List<HighScoreEntry> HighScores = new List<HighScoreEntry>();
    public int MaxHighScores = 5; // Store top 5 scores

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScores();
    }

    public void SaveScore(int score)
    {
        // Add the new score
        HighScores.Add(new HighScoreEntry(PlayerName, score));
        // Sort the list by score in descending order
        HighScores.Sort((a, b) => b.score.CompareTo(a.score));
        // Keep only the top scores
        if (HighScores.Count > MaxHighScores)
        {
            HighScores = HighScores.GetRange(0, MaxHighScores);
        }
        // Save to PlayerPrefs
        SaveHighScores();
    }

    private void SaveHighScores()
    {
        // Convert high scores to JSON
        string json = JsonUtility.ToJson(new HighScoreList { highScores = HighScores });
        PlayerPrefs.SetString("HighScores", json);
        PlayerPrefs.Save();
    }

    private void LoadHighScores()
    {
        if (PlayerPrefs.HasKey("HighScores"))
        {
            string json = PlayerPrefs.GetString("HighScores");
            HighScoreList loadedScores = JsonUtility.FromJson<HighScoreList>(json);
            HighScores = loadedScores.highScores;
        }
    }
}