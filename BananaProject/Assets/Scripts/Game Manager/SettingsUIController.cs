using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class SettingsUIController : MonoBehaviour
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

    [Header("Level Information")]
    [SerializeField] private int[] levelSceneIndices = { 5, 7, 9, 11, 13, 15 };
    [SerializeField] private string[] levelNames = { "Level 1", "Level 2", "Level 3", "Level 4", "Level 5", "Boss Level" };

    [Header("Scene Navigation")]
    [SerializeField] private int mainMenuSceneIndex = 0;

    private SettingsManager settingsManager;

    private void Start()
    {
        settingsManager = SettingsManager.Instance;

        if (settingsManager == null)
        {
            Debug.LogError("SettingsManager not found! Make sure it's in the scene.");
            return;
        }

        //Initialize UI elements with current settings
        InitializeUI();

        //Add listeners to UI elements
        SetupListeners();
    }

    private void InitializeUI()
    {
        fullscreenToggle.isOn = settingsManager.IsFullscreen;
        windowedToggle.isOn = !settingsManager.IsFullscreen;

        masterVolumeSlider.minValue = 0.001f;
        sfxVolumeSlider.minValue = 0.001f;
        musicVolumeSlider.minValue = 0.001f;

        masterVolumeSlider.value = settingsManager.MasterVolume;
        sfxVolumeSlider.value = settingsManager.SFXVolume;
        musicVolumeSlider.value = settingsManager.MusicVolume;

        //Initialize DEV TOOLS
        if (godModeToggle != null)
            godModeToggle.isOn = settingsManager.GodModeEnabled;

        if (levelSelectDropdown != null)
        {
            levelSelectDropdown.ClearOptions();
            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

            for (int i = 0; i < levelNames.Length; i++)
            {
                options.Add(new TMP_Dropdown.OptionData(levelNames[i]));
            }

            levelSelectDropdown.AddOptions(options);
        }
    }

    private void SetupListeners()
    {
        fullscreenToggle.onValueChanged.AddListener(OnFullscreenToggleChanged);
        windowedToggle.onValueChanged.AddListener(OnWindowedToggleChanged);

        masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);

        //Dev tools
        if (godModeToggle != null)
            godModeToggle.onValueChanged.AddListener(OnGodModeToggleChanged);

        if (loadLevelButton != null)
            loadLevelButton.onClick.AddListener(LoadSelectedLevel);
    }

    public void OnFullscreenToggleChanged(bool isOn)
    {
        if (isOn)
        {
            windowedToggle.isOn = false;
            settingsManager.SetFullscreen(true);
            settingsManager.ForceScreenMode(true);
        }
    }

    public void OnWindowedToggleChanged(bool isOn)
    {
        if (isOn)
        {
            fullscreenToggle.isOn = false;
            settingsManager.SetFullscreen(false);
            settingsManager.ForceScreenMode(false);
        }
    }

    private void OnMasterVolumeChanged(float value)
    {
        settingsManager.SetMasterVolume(value);
    }

    private void OnSFXVolumeChanged(float value)
    {
        settingsManager.SetSFXVolume(value);
    }

    private void OnMusicVolumeChanged(float value)
    {
        settingsManager.SetMusicVolume(value);
    }

    public void ApplySettings()
    {
        settingsManager.ApplySettings();
        settingsManager.SaveSettings();
    }

    public void ReturnToMainMenu()
    {
        ApplySettings();
        SceneManager.LoadScene(mainMenuSceneIndex);
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

    //For pause menu
    public void CloseSettingsPanel()
    {
        ApplySettings();
        gameObject.SetActive(false);
    }

    //DEV TOOLS MEHTODS
    private void OnGodModeToggleChanged(bool isOn)
    {
        settingsManager.SetGodMode(isOn);
    }

    public void LoadSelectedLevel()
    {
        if (levelSelectDropdown != null && levelSceneIndices.Length > levelSelectDropdown.value)
        {
            int selectedLevelIndex = levelSceneIndices[levelSelectDropdown.value];
            Time.timeScale = 1f;
            SceneManager.LoadScene(selectedLevelIndex);
        }
    }
}
