using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonManager : MonoBehaviour
{
    [SerializeField] private int mainMenuIndex = 0;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) 
        { 
            ReturnToMainMenu();
        }

    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuIndex);
    }
}
