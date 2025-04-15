using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LiquidArea : MonoBehaviour
{
    public GameObject FailutrePannel;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"  )
        {
            
            Invoke("GameFail",2f);
            other.gameObject.  GetComponent<PlayerController>().enabled = false;
       
          
        }
    }

    void GameFail()
    {
        FailutrePannel.SetActive(true);
        Time.timeScale = 0;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
