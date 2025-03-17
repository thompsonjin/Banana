using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SuccessPannel : MonoBehaviour
{
    public Button Button;
    // Start is called before the first frame update
    void Start()
    {
        Button.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Menu UI");
          
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
