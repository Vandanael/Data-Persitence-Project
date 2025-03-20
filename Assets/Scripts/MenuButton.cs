using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonSetup : MonoBehaviour
{
    public Button menuButton;

    void Start()
    {
        menuButton.onClick.AddListener(GoToMenu);
    }

    void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}