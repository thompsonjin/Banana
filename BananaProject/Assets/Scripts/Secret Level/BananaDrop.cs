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
            pickBananaAudio.Play();
            Destroy(gameObject);
            
        }
    }
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
}
