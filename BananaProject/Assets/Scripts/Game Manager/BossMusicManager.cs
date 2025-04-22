using UnityEngine;
using UnityEngine.SceneManagement;

public class BossMusicManager : MonoBehaviour
{
    public static BossMusicManager Instance { get; private set; }

    private AudioSource bossAudioSource;

    private float musicPosition = 0f;

    private bool bossActivated = false;

    private void Awake()
    {
        
        //Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            bossAudioSource = gameObject.AddComponent<AudioSource>();
            bossAudioSource.loop = true;
            bossAudioSource.playOnAwake = false;
        }
        else
        {
            Destroy(gameObject);
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

    private void Update()
    {
        //Track current position in the music
        if (bossAudioSource.isPlaying)
        {
            musicPosition = bossAudioSource.time;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Boss Fight")
        {
            CleanUp();
        }
        else if (bossActivated)
        {
            ResumeBossMusic();
        }
    }

    public void StartBossMusic(AudioClip musicClip, float volume = 1.0f)
    {
        if (!bossActivated)
        {
            bossAudioSource.clip = musicClip;
            bossAudioSource.volume = volume;
            bossAudioSource.Play();
            bossActivated = true;
        }
    }

    private void ResumeBossMusic()
    {
        if (bossActivated && !bossAudioSource.isPlaying)
        {
            bossAudioSource.time = musicPosition;
            bossAudioSource.Play();
        }
    }

    public bool IsBossActivated()
    {
        return bossActivated;
    }

    private void CleanUp()
    {
        bossAudioSource.Stop();
        bossActivated = false;
        musicPosition = 0f;
        Destroy(gameObject);
        Instance = null;
    }
}