using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingsUIController : MonoBehaviour
{
    [Header("Display Settings")]
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle windowedToggle;

    [Header("Volume Settings")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;

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

        masterVolumeSlider.value = settingsManager.MasterVolume;
        sfxVolumeSlider.value = settingsManager.SFXVolume;
        musicVolumeSlider.value = settingsManager.MusicVolume;
    }

    private void SetupListeners()
    {
        fullscreenToggle.onValueChanged.AddListener(OnFullscreenToggleChanged);
        windowedToggle.onValueChanged.AddListener(OnWindowedToggleChanged);

        masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
    }

    private void OnFullscreenToggleChanged(bool isOn)
    {
        if (isOn)
        {
            windowedToggle.isOn = false;
            settingsManager.SetFullscreen(true);
        }
        else if (!windowedToggle.isOn)
        {
            fullscreenToggle.isOn = true;
        }
    }

    private void OnWindowedToggleChanged(bool isOn)
    {
        if (isOn)
        {
            fullscreenToggle.isOn = false;
            settingsManager.SetFullscreen(false);
        }
        else if (!fullscreenToggle.isOn)
        {
            windowedToggle.isOn = true;
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

    //For pause menu
    public void CloseSettingsPanel()
    {
        ApplySettings();
        gameObject.SetActive(false);
    }
}
