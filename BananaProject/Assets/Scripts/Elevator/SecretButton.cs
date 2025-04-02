using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SecretButton : MonoBehaviour
{
    public GameObject buttonPrompt;
    public GameObject button;

    private bool unlock = false;


    private void Awake()
    {
        if(BananaManager.MaxBananas >= 10)
        {
            unlock = true;
        }
    }

    void Start()
    {
        if (unlock)
        {
            button.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && unlock)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(18);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && unlock)
        {
            buttonPrompt.SetActive(true);         
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && unlock)
        {
            buttonPrompt.SetActive(false);
        }
    }
}
