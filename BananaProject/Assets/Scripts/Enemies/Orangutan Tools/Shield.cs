using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shield : MonoBehaviour
{
    float hitTime;
    float hitTimeMax = 2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && hitTime <= 0)
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage();
            hitTime = hitTimeMax;
        }
    }

    private void Update()
    {
        if(hitTime < 0)
        {
            hitTime -= Time.deltaTime;
        }
    }
}
