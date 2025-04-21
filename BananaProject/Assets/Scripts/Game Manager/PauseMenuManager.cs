using System.Collections.Generic;
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

    [Header("Dev Tools Access")]
    [SerializeField] private string devToolsUnlockCode = "GORI";
    private List<KeyCode> unlockSequence;
    private List<KeyCode> currentSequence = new List<KeyCode>();
    private float sequenceResetTimer = 0f;
    private float maxSequenceResetTime = 2f;
    private bool devToolsUnlocked = false;

    [SerializeField] private InventoryUIManager inventoryUIManager;

    private bool isPaused = false;

    private void Start()
    {
        ResumeGame();
        InitializeUnlockSequence();
    }

    private void InitializeUnlockSequence()
    {
        unlockSequence = new List<KeyCode>();
        foreach (char c in devToolsUnlockCode.ToUpper())
        {
            unlockSequence.Add((KeyCode)System.Enum.Parse(typeof(KeyCode), c.ToString()));
        }

        devToolsUnlocked = PlayerPrefs.GetInt("DevToolsUnlocked", 0) == 1;

        if (settingsPanel != null)
        {
            Transform devToolsContainer = settingsPanel.transform.Find("Dev Tools Container");
            if (devToolsContainer != null)
            {
                devToolsContainer.gameObject.SetActive(devToolsUnlocked);
            }
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (isPaused)
        {
            CheckForUnlockSequence();
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

    private void CheckForUnlockSequence()
    {
        if (currentSequence.Count > 0)
        {
            sequenceResetTimer += Time.unscaledDeltaTime;
            if (sequenceResetTimer > maxSequenceResetTime)
            {
                currentSequence.Clear();
                sequenceResetTimer = 0f;
            }
        }

        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                if (key == KeyCode.Escape || key == KeyCode.UpArrow || key == KeyCode.DownArrow ||
                    key == KeyCode.LeftArrow || key == KeyCode.RightArrow || key == KeyCode.Return)
                    continue;

                currentSequence.Add(key);
                sequenceResetTimer = 0f;

                if (SequenceMatches())
                {
                    ToggleDevTools();
                    currentSequence.Clear();
                }

                if (currentSequence.Count > unlockSequence.Count)
                {
                    currentSequence.RemoveAt(0);
                }

                break;
            }
        }
    }

    private bool SequenceMatches()
    {
        if (currentSequence.Count != unlockSequence.Count)
            return false;

        for (int i = 0; i < unlockSequence.Count; i++)
        {
            if (currentSequence[i] != unlockSequence[i])
                return false;
        }

        return true;
    }

    private void ToggleDevTools()
    {
        devToolsUnlocked = !devToolsUnlocked;

        PlayerPrefs.SetInt("DevToolsUnlocked", devToolsUnlocked ? 1 : 0);
        PlayerPrefs.Save();

        if (settingsPanel != null)
        {
            Transform devToolsContainer = settingsPanel.transform.Find("Dev Tools Container");
            if (devToolsContainer != null)
            {
                devToolsContainer.gameObject.SetActive(devToolsUnlocked);

                if (devToolsUnlocked)
                    Debug.Log("Dev Tools Unlocked!");
                else
                    Debug.Log("Dev Tools Locked!");
            }
        }
    }


}
