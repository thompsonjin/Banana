using UnityEngine;
using UnityEngine.SceneManagement;

public class DevToolsHintController : MonoBehaviour
{
    [SerializeField] private int deathsBeforeHint = 5;
    [SerializeField] private string bossLevelName = "Boss Fight";

    private int initialDeathCount;
    private bool hintShown = false;
    private Canvas hintCanvas;
    private bool hasPlayerSeenHint = false;

    private void Awake()
    {
        hintCanvas = GetComponent<Canvas>();

        if (hintCanvas == null)
        {
            Debug.LogError("No Canvas component found on this GameObject. DevToolsHintController needs to be attached to a Canvas.");
            return;
        }

        hasPlayerSeenHint = PlayerPrefs.GetInt("DevToolsHintShown", 0) == 1;

        hintCanvas.enabled = false;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != bossLevelName)
        {
            this.enabled = false;
            return;
        }

        if (PlayerPrefs.HasKey("InitialBossDeathCount"))
        {
            initialDeathCount = PlayerPrefs.GetInt("InitialBossDeathCount");
        }
        else
        {
            initialDeathCount = ScoreTracker.deaths;
            PlayerPrefs.SetInt("InitialBossDeathCount", initialDeathCount);
            PlayerPrefs.Save();
        }
    }

    private void Update()
    {
        if (!hasPlayerSeenHint && !hintShown && ScoreTracker.deaths >= initialDeathCount + deathsBeforeHint)
        {
            ShowHint();
        }

        if (hintShown && (Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
        {
            HideHint();
        }
    }

    private void ShowHint()
    {
        hintCanvas.enabled = true;
        hintShown = true;

        Time.timeScale = 0.00001f;
    }

    private void HideHint()
    {
        hintCanvas.enabled = false;
        hintShown = false;
        hasPlayerSeenHint = true;

        Time.timeScale = 1f;

        PlayerPrefs.SetInt("DevToolsHintShown", 1);
        PlayerPrefs.Save();
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
        if (scene.name == bossLevelName)
        {
            hasPlayerSeenHint = PlayerPrefs.GetInt("DevToolsHintShown", 0) == 1;

            hintShown = false;
            if (hintCanvas != null)
            {
                hintCanvas.enabled = false;
            }

            Time.timeScale = 1f;
        }
    }


    //TESTING
    public void ResetHintForTesting()
    {
        PlayerPrefs.DeleteKey("DevToolsHintShown");
        PlayerPrefs.DeleteKey("InitialBossDeathCount");
        PlayerPrefs.Save();

        hasPlayerSeenHint = false;
        initialDeathCount = ScoreTracker.deaths;

        Debug.Log("Dev Tools Hint has been reset for testing!");
    }
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetHintForTesting();
        }
    }
}
