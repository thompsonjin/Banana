using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boob : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {


            Time.timeScale = 0;
            BananaScore.instance.FailutrePannel.SetActive(true);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.down*5f*Time.deltaTime);
    }
}
