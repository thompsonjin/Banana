using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuPannel : MonoBehaviour
{


    public Button newGame;
    public Button Setting;
    public Button exit;
    public Button saves;
    public Button credit;
    public GameObject savePannel;
    public GameObject settingpannel;
    public GameObject creditpannel;
    // Start is called before the first frame update
    void Start()
    {
        newGame.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("LVL 1");

             


        });
        Setting.onClick.AddListener(() =>
        {

            settingpannel.SetActive(true);



        });
        exit.onClick.AddListener(() =>
        {


            Application.Quit();


        });
        saves.onClick.AddListener(() =>
        {

          savePannel.SetActive(true);



        });
        credit.onClick.AddListener(() =>
        {
            
          
            creditpannel.SetActive(true);



        });


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
