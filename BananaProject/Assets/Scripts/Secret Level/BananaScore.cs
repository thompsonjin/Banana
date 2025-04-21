using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class BananaScore : MonoBehaviour
{
    public int bananaScore=0;
    public  static BananaScore instance;
    public float timer = 50f;
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

 
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;


      
        if (timer < 0f)
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
