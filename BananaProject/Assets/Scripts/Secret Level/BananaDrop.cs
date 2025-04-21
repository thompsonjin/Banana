using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaDrop : MonoBehaviour
{
    public AudioSource pickBananaAudio;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BananaScore.instance.bananaScore++;
            other.gameObject.GetComponent<PlayerController>().GiveBanana(1);
            GameObject.Find("ScrectLVL Manager").GetComponent<BananaScore>().AddToTimer(2);
            pickBananaAudio.Play();
            Destroy(gameObject);
            
        }
    }
}
