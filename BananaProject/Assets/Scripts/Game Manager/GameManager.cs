using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
//using Scene = UnityEngine.SceneManagement.Scene;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            CheckpointManager.CheckpointReset();
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex + 1);
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
}
