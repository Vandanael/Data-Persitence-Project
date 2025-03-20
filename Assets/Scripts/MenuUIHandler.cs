using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Add this for TextMeshPro

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField nameInputField; // Changed from InputField to TMP_InputField

    public void StartGame()
    {
        if (DataManager.Instance != null)
        {
            // Store the player's name
            DataManager.Instance.PlayerName = nameInputField.text;
        }

        // Load the main scene
        SceneManager.LoadScene(1); // Make sure your Main scene is at index 1 in Build Settings
    }
}