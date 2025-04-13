using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class BananaScore : MonoBehaviour
{
    public int bananaScore=0;
    public  static BananaScore instance;
    public float duringTime = 50f;
    public GameObject sucessPannel;
    public GameObject FailutrePannel;

    public CinemachineVirtualCamera camera;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
    
    }
<<<<<<< Updated upstream
    
=======

 
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
    // Update is called once per frame
    void Update()
    {
        duringTime -= Time.deltaTime;


      
        if (duringTime<0f)
        {
            if (bananaScore>=120)
            {
                //success
                Time.timeScale = 0;
                sucessPannel.SetActive(true);
            }
            else
            {
                FailutrePannel.SetActive(true);
                Time.timeScale = 0;
                //fail
            }
        }
        else
        {
            if (bananaScore>=120)
            {
                //success
                Time.timeScale = 0;
                sucessPannel.SetActive(true);
            }
        }
    }
}
