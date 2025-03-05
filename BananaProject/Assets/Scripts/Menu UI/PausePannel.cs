using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePannel : MonoBehaviour
{
    public Button resume;
    public Button  inventor;
    public Button setting;
    public Button save;
    public Button quit;
    public GameObject savePannel;
  public GameObject SettingsPannel;
    void Start()
    {
        resume.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;



        });
        inventor.onClick.AddListener(() =>
        {
          
            
            
            
            
        });
        setting.onClick.AddListener(() =>
        {
          
            SettingsPannel.SetActive(true);
            gameObject.SetActive(false);
            
            
        });
        save.onClick.AddListener(() =>
        {
          
            savePannel.SetActive(true);
            
            gameObject.SetActive(false);
            
        });
        quit.onClick.AddListener(() =>
        {

            gameObject.SetActive(false);
            SceneManager.LoadScene("Menu UI");



        });
    }

 
}
