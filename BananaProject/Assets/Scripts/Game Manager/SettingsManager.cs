using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

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
    private const string GOD_MODE_KEY = "GodMode"; //This is only a dev tool -NOTE TO DELETE LATER

    //Dev tools
    private bool godModeEnabled = false;
    public bool GodModeEnabled => godModeEnabled;

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

        masterVolume = PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, 0.75f);
        sfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 0.75f);
        musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 0.75f);

        //Dev tool
        godModeEnabled = PlayerPrefs.GetInt(GOD_MODE_KEY, 0) == 1;

        ApplySettings();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt(FULLSCREEN_KEY, isFullscreen ? 1 : 0);

        PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, masterVolume);
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, sfxVolume);
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, musicVolume);

        //Dev tool
        PlayerPrefs.SetInt(GOD_MODE_KEY, godModeEnabled ? 1 : 0);

        PlayerPrefs.Save();
    }

    public void ApplySettings()
    {
        int width = Screen.width;
        int height = Screen.height;

        if (Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.LinuxPlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            Screen.fullScreenMode = isFullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
            Screen.SetResolution(width, height, Screen.fullScreenMode);
        }
        else
        {
            Screen.fullScreen = isFullscreen;
        }

        if (audioMixer != null)
        {
            audioMixer.SetFloat("MasterVolume", ConvertToDecibel(masterVolume));
            audioMixer.SetFloat("SFXVolume", ConvertToDecibel(sfxVolume));
            audioMixer.SetFloat("MusicVolume", ConvertToDecibel(musicVolume));
        }

        Debug.Log($"Applied screen mode: {(isFullscreen ? "Fullscreen" : "Windowed")} at {width}x{height}");
    }

    public void ForceScreenMode(bool fullscreen)
    {
        isFullscreen = fullscreen;

        FullScreenMode mode = fullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;

        int width = Screen.width;
        int height = Screen.height;

        Screen.SetResolution(width, height, mode);

        PlayerPrefs.SetInt(FULLSCREEN_KEY, fullscreen ? 1 : 0);
        PlayerPrefs.Save();

        Debug.Log($"Forced screen mode: {(fullscreen ? "Fullscreen" : "Windowed")} at {width}x{height}");
    }

    //Getters and setters for the UI to use
    public bool IsFullscreen => isFullscreen;

    public void SetFullscreen(bool value)
    {
        isFullscreen = value;
    }

    public float MasterVolume => masterVolume;
    public float SFXVolume => sfxVolume;
    public float MusicVolume => musicVolume;

    public void SetMasterVolume(float value)
    {
        masterVolume = value;
        if (audioMixer != null)
        {
            float dbValue = ConvertToDecibel(value);
            audioMixer.SetFloat("MasterVolume", dbValue);
        }
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
        if (audioMixer != null)
        {
            float dbValue = ConvertToDecibel(value);
            audioMixer.SetFloat("SFXVolume", dbValue);
        }
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        if (audioMixer != null)
        {
            float dbValue = ConvertToDecibel(value);
            audioMixer.SetFloat("MusicVolume", dbValue);
        }
    }

    private float ConvertToDecibel(float value)
    {
        if (value <= 0.001f)
            return -80f;

        return Mathf.Log10(value) * 20f;
    }

    public void SetDefaultVolumes()
    {
        masterVolume = 0.75f;
        sfxVolume = 0.75f;
        musicVolume = 0.75f;

        ApplySettings();
        SaveSettings();
    }

    public void ValidateAudioMixer()
    {
        if (audioMixer == null) return;

        float currentMaster, currentSFX, currentMusic;
        audioMixer.GetFloat("MasterVolume", out currentMaster);
        audioMixer.GetFloat("SFXVolume", out currentSFX);
        audioMixer.GetFloat("MusicVolume", out currentMusic);

        if (currentMaster <= -79f && masterVolume > 0.001f)
        {
            audioMixer.SetFloat("MasterVolume", ConvertToDecibel(masterVolume));
        }

        if (currentSFX <= -79f && sfxVolume > 0.001f)
        {
            audioMixer.SetFloat("SFXVolume", ConvertToDecibel(sfxVolume));
        }

        if (currentMusic <= -79f && musicVolume > 0.001f)
        {
            audioMixer.SetFloat("MusicVolume", ConvertToDecibel(musicVolume));
        }
    }


    //Dev tool methods
    public void ToggleGodMode()
    {
        godModeEnabled = !godModeEnabled;
        SaveSettings();
    }

    public void SetGodMode(bool value)
    {
        godModeEnabled = value;
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
