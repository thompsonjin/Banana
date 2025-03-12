using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    //Scene indices - update these to match your build settings
    [Header("Scene Indices")]
    [SerializeField] private int firstLevelIndex = 5;
    [SerializeField] private int savesMenuIndex = 2;
    [SerializeField] private int settingsMenuIndex = 3;
    [SerializeField] private int creditsScreenIndex = 4;

    public void StartNewGame()
    {
        SceneManager.LoadScene(firstLevelIndex);
    }

    public void OpenSaves()
    {
        SceneManager.LoadScene(savesMenuIndex);
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene(settingsMenuIndex);
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene(creditsScreenIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}