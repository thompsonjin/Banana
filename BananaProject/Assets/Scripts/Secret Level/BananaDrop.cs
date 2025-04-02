using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaDrop : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BananaScore.instance.bananaScore++;
            Destroy(gameObject);
        }
    }

}
