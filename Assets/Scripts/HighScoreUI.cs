using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreUI : MonoBehaviour
{
    public GameObject highScoreEntryPrefab;
    public Transform highScoreContainer;

    private void Start()
    {
        UpdateHighScoreUI();
    }

    public void UpdateHighScoreUI()
    {
        // Clear existing entries
        foreach (Transform child in highScoreContainer)
        {
            Destroy(child.gameObject);
        }

        // Add new entries
        if (DataManager.Instance != null && DataManager.Instance.HighScores.Count > 0)
        {
            for (int i = 0; i < DataManager.Instance.HighScores.Count; i++)
            {
                GameObject entryObject = Instantiate(highScoreEntryPrefab, highScoreContainer);
                TextMeshProUGUI entryText = entryObject.GetComponent<TextMeshProUGUI>();

                HighScoreEntry entry = DataManager.Instance.HighScores[i];
                entryText.text = $"{i + 1}. {entry.playerName}: {entry.score}";
            }
        }
    }
}