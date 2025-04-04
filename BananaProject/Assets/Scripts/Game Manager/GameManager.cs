using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
//using Scene = UnityEngine.SceneManagement.Scene;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool transition;
    ScoreTracker s_Track;
    private PlayerController player;

    private void Awake()
    {
        //Code to make sure there are no Game manager duplicates as well as load it to other scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        s_Track = GameObject.FindWithTag("Score").GetComponent<ScoreTracker>();

        BananaCoinCheck();
    }

    public void OnPLayerDeath()
    {
        StartCoroutine(HandlePlayerDeath());
    }

    private IEnumerator HandlePlayerDeath()
    {
        yield return new WaitForSeconds(2);

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
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
        player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            PowersForCurrentLevel();
        }
    }

    private void PowersForCurrentLevel()
    {
        DisableAllPowers();

        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "LVL 1":
                break;

            case "LVL 2":
                player.EnableShadowKick();
                break;

            case "LVL 2.5":
                player.EnableShadowKick();
                break;

            case "LVL 3":
                player.EnableShadowKick();
                player.EnableCharge();
                break;

            case "LVL 3.5":
                player.EnableShadowKick();
                player.EnableCharge();
                break;

            case "LVL 4":
                player.EnableShadowKick();
                player.EnableCharge();
                player.EnableGroundPound();
                break;

            case "LVL 4.5":
                player.EnableShadowKick();
                player.EnableCharge();
                player.EnableGroundPound();
                player.EnableBananaGun();
                break;

            case "LVL 5":
                player.EnableShadowKick();
                player.EnableCharge();
                player.EnableGroundPound();
                player.EnableBananaGun();
                player.EnableBananaShield();
                break;

            case "LVL 5.5":
                player.EnableShadowKick();
                player.EnableCharge();
                player.EnableGroundPound();
                player.EnableBananaGun();
                player.EnableBananaShield();
                break;

            case "Boss Fight":
                player.EnableShadowKick();
                player.EnableCharge();
                player.EnableGroundPound();
                player.EnableBananaGun();
                player.EnableBananaShield();
                break;

            case "ScrectLVL":
                player.EnableShadowKick();
                player.EnableCharge();
                player.EnableGroundPound();
                player.EnableBananaGun();
                player.EnableBananaShield();
                break;

            default:
                break;
        }
        
        
        UpdateInventoryUI();
    }
    
    private void UpdateInventoryUI()
    {
        InventoryUIManager[] inventoryManagers = FindObjectsOfType<InventoryUIManager>();
        foreach (InventoryUIManager manager in inventoryManagers)
        {
            manager.SetPlayerController(player);
        }
    }

    private void DisableAllPowers()
    {
        if (player != null)
        {
            player.DisableShadowKick();
            player.DisableCharge();
            player.DisableGroundPound();
            player.DisableBananaGun();
            player.DisableBananaShield();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            CheckpointManager.CheckpointReset();
            Scene currentScene = SceneManager.GetActiveScene();
            if(currentScene.buildIndex == 5 || currentScene.buildIndex == 7|| currentScene.buildIndex == 9|| currentScene.buildIndex == 11|| currentScene.buildIndex == 13 || currentScene.buildIndex == 15)
            {
                SceneManager.LoadScene(17);

                if (currentScene.buildIndex == 15)
                {
                    s_Track.SetBossWin();
                }
            }
            else
            {
                SceneManager.LoadScene(currentScene.buildIndex + 1);
                s_Track.ResetScore();
            }
                
        }
    }

    void BananaCoinCheck()
    {
        if(GetScene() == 5 && BananaManager.one)
        {
            Destroy(GameObject.FindWithTag("Banana Coin"));
        }

        if (GetScene() == 6 && BananaManager.two)
        {
            Destroy(GameObject.FindWithTag("Banana Coin"));
        }

        if (GetScene() == 7 && BananaManager.three)
        {
            Destroy(GameObject.FindWithTag("Banana Coin"));
        }

        if (GetScene() == 8 && BananaManager.four)
        {
            Destroy(GameObject.FindWithTag("Banana Coin"));
        }

        if (GetScene() == 9 && BananaManager.five)
        {
            Destroy(GameObject.FindWithTag("Banana Coin"));
        }
    }

    public int GetScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void NextScene()
    {
        CheckpointManager.CheckpointReset();
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 1);
    }


}
