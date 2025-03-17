using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaScore : MonoBehaviour
{
    public int bananaScore=0;
    public  static BananaScore instance;
    public float duringTime = 50f;
    public GameObject bananaPrefab;
    public GameObject boob;

    public GameObject sucessPannel;
    public GameObject FailutrePannel;
    // Start is called before the first frame update
    void Start()
    {
        if (instance==null)
        {
            instance = this;
        }
    
    }

    public void CreatDrop()
    {
        
        
        
        if (Random.Range(0,1f)>0.5f)
        {
            //Éú³ÉÏã½¶
            Instantiate(bananaPrefab, new Vector3(Random.Range(-11.8f,11.9f),12.7f+Random.Range(0f,3f),0f), Quaternion.identity);
        }
        else
        {
            Instantiate(boob,new Vector3(Random.Range(-11.8f,11.9f),12.7f+Random.Range(0f,3f),0f), Quaternion.identity);
        }
    }
    private float timer = 0;
    // Update is called once per frame
    void Update()
    {
        duringTime -= Time.deltaTime;
        timer += Time.deltaTime;
        if (timer>1.5f)
        {
            float t = Random.Range(1, 4);
            for (int i = 0; i < t; i++)
            {
                CreatDrop();
            }
       
            
            timer = 0;
        }

        if (duringTime<0f)
        {
            if (bananaScore>=24)
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
    }
}
