using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingPannel : MonoBehaviour
{

    public Toggle fullscreen;
    public Toggle Windows;
    public Slider MASTERVOLUME;
    public Slider SOUNDFX;
    public Slider MUSIC;
    public Button RETURN;

    public GameObject pausePannel;
    // Start is called before the first frame update
    void Start()
    {
        fullscreen.onValueChanged.AddListener((e) =>
        {
            Screen.fullScreen = e;
            


        });
        Windows.onValueChanged.AddListener((e) =>
        {
            
            Screen.fullScreen = e;
            if (e==true)
            {
                Screen.fullScreenMode = FullScreenMode.Windowed;
            }
        

        });
        
        MASTERVOLUME.onValueChanged.AddListener((e) =>
        {
            
            
            
        });
        SOUNDFX.onValueChanged.AddListener((e) =>
        {
            
            
            
        });
        
        MUSIC.onValueChanged.AddListener((e) =>
        {
            
            
            
        });
        RETURN.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            if (SceneManager.GetActiveScene().buildIndex==0)
            {
                
            }
            else  if( SceneManager.GetActiveScene().buildIndex==1 )
            {
                pausePannel.SetActive(true);
            }
          
            
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
