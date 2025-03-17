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

        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.down*5f*Time.deltaTime);
    }
}
