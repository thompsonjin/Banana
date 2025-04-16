using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private AudioSource audioSource;

    //List of scenes where music should play
    private readonly string[] menuSceneNames = {"StartScreen", "Intro Comic 1", "MainMenu", "CreditScreen", "SettingsScreen" };

    private void Awake()
    {
        //Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!ShouldPlayInScene(scene.name))
        {
            //Stop the music if we're not in a menu scene
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
        else
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    private bool ShouldPlayInScene(string sceneName)
    {
        foreach (string menuScene in menuSceneNames)
        {
            if (sceneName == menuScene)
            {
                return true;
            }
        }

        return false;
    }
}
