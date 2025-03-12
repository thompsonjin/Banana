using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    [Header("Audio Mixers")]
    [SerializeField] private AudioMixer audioMixer;

    //Default settings values
    private bool isFullscreen = true;
    private float masterVolume = 1.0f;
    private float sfxVolume = 1.0f;
    private float musicVolume = 1.0f;

    //Playerprefs keys
    private const string FULLSCREEN_KEY = "Fullscreen";
    private const string MASTER_VOLUME_KEY = "MasterVolume";
    private const string SFX_VOLUME_KEY = "SFXVolume";
    private const string MUSIC_VOLUME_KEY = "MusicVolume";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadSettings()
    {
        isFullscreen = PlayerPrefs.GetInt(FULLSCREEN_KEY, 1) == 1;

        masterVolume = PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, 1.0f);
        sfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 1.0f);
        musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1.0f);

        ApplySettings();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt(FULLSCREEN_KEY, isFullscreen ? 1 : 0);

        PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, masterVolume);
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, sfxVolume);
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, musicVolume);

        PlayerPrefs.Save();
    }

    public void ApplySettings()
    {
        Screen.fullScreen = isFullscreen;

        if (audioMixer != null)
        {
            audioMixer.SetFloat("MasterVolume", ConvertToDecibel(masterVolume));
            audioMixer.SetFloat("SFXVolume", ConvertToDecibel(sfxVolume));
            audioMixer.SetFloat("MusicVolume", ConvertToDecibel(musicVolume));
        }
    }

    //Getters and setters for the UI to use
    public bool IsFullscreen => isFullscreen;

    public void SetFullscreen(bool value)
    {
        isFullscreen = value;
    }

    public float MasterVolume => masterVolume;

    public void SetMasterVolume(float value)
    {
        masterVolume = value;
    }

    public float SFXVolume => sfxVolume;

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
    }

    public float MusicVolume => musicVolume;

    public void SetMusicVolume(float value)
    {
        musicVolume = value;
    }

    private float ConvertToDecibel(float value)
    {
        if (value <= 0)
            return -80f;

        //Convert to decibel scale
        return Mathf.Log10(value) * 20f;
    }
}
