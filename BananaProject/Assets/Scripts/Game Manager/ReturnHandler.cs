using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnHandler : MonoBehaviour
{
    [SerializeField] private bool isSettingsOrSavesScene = true;

    void Start()
    {
        if (isSettingsOrSavesScene)
        {
            CheckForReturnToGame();
        }
    }

    private void CheckForReturnToGame()
    {
        int wasPaused = PlayerPrefs.GetInt("WasPaused", 0);
        int returnIndex = PlayerPrefs.GetInt("ReturnSceneIndex", -1);
    }

    public void ReturnToPreviousScene()
    {
        int returnIndex = PlayerPrefs.GetInt("ReturnSceneIndex", 1);

        PlayerPrefs.DeleteKey("WasPaused");
        PlayerPrefs.DeleteKey("ReturnSceneIndex");

        SceneManager.LoadScene(returnIndex);
    }
}