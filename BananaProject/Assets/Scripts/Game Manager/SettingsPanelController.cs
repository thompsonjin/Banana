using UnityEngine;
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
            UpdateUI();
        }
        else
        {
            Debug.LogError("SettingsManager not found!");
        }
    }

    private void UpdateUI()
    {
        fullscreenToggle.isOn = settingsManager.IsFullscreen;
        windowedToggle.isOn = !settingsManager.IsFullscreen;

        masterVolumeSlider.value = settingsManager.MasterVolume;
        sfxVolumeSlider.value = settingsManager.SFXVolume;
        musicVolumeSlider.value = settingsManager.MusicVolume;
    }

    //Handle UI events
    public void OnFullscreenToggleChanged(bool isOn)
    {
        if (isOn)
        {
            windowedToggle.isOn = false;
            settingsManager.SetFullscreen(true);
        }
    }

    public void OnWindowedToggleChanged(bool isOn)
    {
        if (isOn)
        {
            fullscreenToggle.isOn = false;
            settingsManager.SetFullscreen(false);
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
        settingsManager.ApplySettings();
        settingsManager.SaveSettings();
    }

    //Back button handler
    public void OnBackButton()
    {
        ApplySettings();
        if (pauseMenuManager != null)
        {
            pauseMenuManager.CloseSettings();
        }
    }
}