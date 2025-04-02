using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [Header("Menu Panels")]
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject backgroundBlur;

    [Header("Scene Indices")]
    [SerializeField] private int mainMenuIndex = 1;
    [SerializeField] private int savesMenuIndex = 2;

    [SerializeField] private InventoryUIManager inventoryUIManager;

    private bool isPaused = false;

    private void Start()
    {
        ResumeGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    //Toggle pause
    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    //Pause
    public void PauseGame()
    {
        Time.timeScale = 0f;

        pauseMenuPanel.SetActive(true);
        backgroundBlur.SetActive(true);

        isPaused = true;
    }

    //Resume the game
    public void ResumeGame()
    {
        Time.timeScale = 1f;

        pauseMenuPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        settingsPanel.SetActive(false);
        backgroundBlur.SetActive(false);

        isPaused = false;
    }

    //Open inventory screen
    public void OpenInventory()
    {
        Debug.Log("OpenInventory called!");
        pauseMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        inventoryPanel.SetActive(true);

        if (inventoryUIManager != null)
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                Debug.Log("Found player controller for inventory");
                inventoryUIManager.SetPlayerController(player);
                inventoryUIManager.UpdateInventoryUI();
            }
            else
            {
                Debug.LogWarning("Could not find PlayerController for inventory!");
            }
        }
        else
        {
            Debug.LogWarning("No InventoryUIManager assigned to PauseMenuManager!");
        }

        Debug.Log("Inventory active state: " + inventoryPanel.activeInHierarchy);
    }

    public void CloseInventory()
    {
        pauseMenuPanel.SetActive(true);
        inventoryPanel.SetActive(false);
    }

    //Open Settings Panel
    public void OpenSettings()
    {
        Debug.Log("OpenSettings called!");
        pauseMenuPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        settingsPanel.SetActive(true);
        Debug.Log("Settings active state: " + settingsPanel.activeInHierarchy);
    }

    //Close Settings Panel
    public void CloseSettings()
    {
        pauseMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    //Open Saves
    public void OpenSaves()
    {
        PlayerPrefs.SetInt("WasPaused", 1);
        PlayerPrefs.SetInt("ReturnSceneIndex", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save();

        SceneManager.LoadScene(savesMenuIndex);
    }

    //Quit to main menu
    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuIndex);
    }
}
