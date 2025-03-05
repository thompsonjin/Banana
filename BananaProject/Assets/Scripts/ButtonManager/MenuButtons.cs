using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("LVL 1");
    }

    public void OpenSaves()
    {
        Debug.Log("Open the saves interface");
        // can add specific logic here, such as loading the save list, displaying save information, etc.
    }

    public void OpenSettings()
    {
        Debug.Log("Open the settings interface");
        //can add specific logic here, such as opening the settings window, adjusting game parameters, etc.
    }

    
    public void ShowCredits()
    {
        Debug.Log("Show the credits information");
        // can add specific logic here, such as loading the credits scene, displaying scrolling subtitles, etc.
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
