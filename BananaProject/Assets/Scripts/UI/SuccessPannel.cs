using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SuccessPannel : MonoBehaviour
{
    public Button Button;

    public Button BossBtn;
    // Start is called before the first frame update
    void Start()
    {
        Button.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
          
        });
        BossBtn.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Boss Fight");
          
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
