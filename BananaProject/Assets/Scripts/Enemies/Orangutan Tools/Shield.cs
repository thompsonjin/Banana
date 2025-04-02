using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shield : MonoBehaviour
{
    float hitTime;
    float hitTimeMax = 2;
    public SpriteRenderer sprite;

    private void Start()
    {
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
    }
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
            sprite.color = Color.gray;
            hitTime -= Time.deltaTime;
        }
        else
        {
            sprite.color = Color.blue;
        }
    }
}
