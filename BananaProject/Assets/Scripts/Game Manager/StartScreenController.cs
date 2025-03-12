using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenController : MonoBehaviour
{
    [SerializeField] private int mainMenuSceneIndex = 1;

    private bool hasTransitioned = false;

    private void Update()
    {
        //Check for any key press or mouse click
        if (!hasTransitioned && (Input.anyKeyDown || Input.GetMouseButtonDown(0)))
        {
            hasTransitioned = true;
            LoadMainMenu();
        }
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneIndex);
    }
}
