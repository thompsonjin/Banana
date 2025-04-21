using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsPanelController : MonoBehaviour
{
    [Header("Display Settings")]
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle windowedToggle;

    [Header("Volume Settings")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;

    [Header("Dev Tools")]
    [SerializeField] private Toggle godModeToggle;
    [SerializeField] private GameObject devToolsContainer;
    [SerializeField] private TMP_Dropdown levelSelectDropdown;
    [SerializeField] private Button loadLevelButton;
    [SerializeField] private string[] levelSceneNames = { "LVL 1", "LVL 2", "LVL 3", "LVL 4", "LVL 5", "Boss Fight" };
    [SerializeField] private string[] displayLevelNames = { "Level 1", "Level 2", "Level 3", "Level 4", "Level 5", "Boss Fight" };
    //[SerializeField] private int[] levelSceneIndices = { 5, 7, 9, 11, 13, 15 };
    //[SerializeField] private string[] levelNames = { "Level 1", "Level 2", "Level 3", "Level 4", "Level 5", "Boss Level" };

    [Header("References")]
    [SerializeField] private PauseMenuManager pauseMenuManager;

    private SettingsManager settingsManager;

    private void Awake()
    {
        if (pauseMenuManager == null)
            pauseMenuManager = GetComponentInParent<PauseMenuManager>();
    }

    private void OnEnable()
    {
        settingsManager = SettingsManager.Instance;

        if (settingsManager != null)
        {
            settingsManager.ValidateAudioMixer();

            fullscreenToggle.isOn = settingsManager.IsFullscreen;
            windowedToggle.isOn = !settingsManager.IsFullscreen;

            //Remove existing listeners
            masterVolumeSlider.onValueChanged.RemoveAllListeners();
            sfxVolumeSlider.onValueChanged.RemoveAllListeners();
            musicVolumeSlider.onValueChanged.RemoveAllListeners();
            fullscreenToggle.onValueChanged.RemoveAllListeners();
            windowedToggle.onValueChanged.RemoveAllListeners();

            //Add listeners
            masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
            sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
            fullscreenToggle.onValueChanged.AddListener(OnFullscreenToggleChanged);
            windowedToggle.onValueChanged.AddListener(OnWindowedToggleChanged);

        
            bool devToolsUnlocked = PlayerPrefs.GetInt("DevToolsUnlocked", 0) == 1;
            if (devToolsContainer != null)
            {
                devToolsContainer.SetActive(devToolsUnlocked);
            }

            UpdateUI();
        }



        else
        {
            Debug.LogError("SettingsManager not found!");
        }
    }

    private void UpdateUI()
    {
        masterVolumeSlider.minValue = 0.001f;
        sfxVolumeSlider.minValue = 0.001f;
        musicVolumeSlider.minValue = 0.001f;

        masterVolumeSlider.value = settingsManager.MasterVolume;
        sfxVolumeSlider.value = settingsManager.SFXVolume;
        musicVolumeSlider.value = settingsManager.MusicVolume;

        //Update DEV TOOLS
        if (godModeToggle != null)
            godModeToggle.isOn = settingsManager.GodModeEnabled;

        if (levelSelectDropdown != null)
        {
            levelSelectDropdown.ClearOptions();
            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

            for (int i = 0; i < displayLevelNames.Length; i++)
            {
                options.Add(new TMP_Dropdown.OptionData(displayLevelNames[i]));
            }

            levelSelectDropdown.AddOptions(options);
        }
    }

    //Handle UI events
    public void OnFullscreenToggleChanged(bool isOn)
    {
        if (isOn)
        {
            windowedToggle.onValueChanged.RemoveAllListeners();
            windowedToggle.isOn = false;
            windowedToggle.onValueChanged.AddListener(OnWindowedToggleChanged);

            settingsManager.SetFullscreen(true);
            settingsManager.ForceScreenMode(true);

            Debug.Log("Pause Menu: Changed to Fullscreen");
        }
        else if (!windowedToggle.isOn)
        {
            fullscreenToggle.isOn = true;
        }
    }
    public void OnWindowedToggleChanged(bool isOn)
    {
        if (isOn)
        {
            fullscreenToggle.onValueChanged.RemoveAllListeners();
            fullscreenToggle.isOn = false;
            fullscreenToggle.onValueChanged.AddListener(OnFullscreenToggleChanged);

            settingsManager.SetFullscreen(false);
            settingsManager.ForceScreenMode(false);

            Debug.Log("Pause Menu: Changed to Windowed");
        }
        else if (!fullscreenToggle.isOn)
        {
            windowedToggle.isOn = true;
        }
    }

    private void EnsureOneToggleSelected()
    {
        if (!fullscreenToggle.isOn && !windowedToggle.isOn)
        {
            fullscreenToggle.isOn = settingsManager.IsFullscreen;
            windowedToggle.isOn = !settingsManager.IsFullscreen;
        }
        else if (fullscreenToggle.isOn && windowedToggle.isOn)
        {
            bool current = settingsManager.IsFullscreen;
            fullscreenToggle.isOn = current;
            windowedToggle.isOn = !current;
        }
    }

    public void OnMasterVolumeChanged(float value)
    {
        settingsManager.SetMasterVolume(value);
    }

    public void OnSFXVolumeChanged(float value)
    {
        settingsManager.SetSFXVolume(value);
    }

    public void OnMusicVolumeChanged(float value)
    {
        settingsManager.SetMusicVolume(value);
    }

    public void ApplySettings()
    {
        EnsureOneToggleSelected();

        settingsManager.SetMasterVolume(masterVolumeSlider.value);
        settingsManager.SetSFXVolume(sfxVolumeSlider.value);
        settingsManager.SetMusicVolume(musicVolumeSlider.value);

        settingsManager.ApplySettings();
        settingsManager.SaveSettings();
    }

    public void ResetAudioSettings()
    {
        if (settingsManager != null)
        {
            settingsManager.SetDefaultVolumes();

            masterVolumeSlider.value = settingsManager.MasterVolume;
            sfxVolumeSlider.value = settingsManager.SFXVolume;
            musicVolumeSlider.value = settingsManager.MusicVolume;
        }
    }

    public void OnGodModeToggleChanged(bool isOn)
    {
        if (settingsManager != null)
        {
            settingsManager.SetGodMode(isOn);
        }
    }

    public void LoadSelectedLevel()
    {
        if (levelSelectDropdown != null && levelSceneNames.Length > levelSelectDropdown.value)
        {
            string selectedLevelName = levelSceneNames[levelSelectDropdown.value];
            Time.timeScale = 1f;
            settingsManager.LoadLevel(selectedLevelName);
        }
    }

    //Back button handler
    public void OnBackButton()
    {
        EnsureOneToggleSelected();

        settingsManager.SetMasterVolume(masterVolumeSlider.value);
        settingsManager.SetSFXVolume(sfxVolumeSlider.value);
        settingsManager.SetMusicVolume(musicVolumeSlider.value);

        ApplySettings();
        if (pauseMenuManager != null)
        {
            pauseMenuManager.CloseSettings();
        }
    }
}