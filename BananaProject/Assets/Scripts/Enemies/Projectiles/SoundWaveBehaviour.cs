using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaveBehaviour : MonoBehaviour
{
  
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 player = collision.gameObject.transform.position;
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            PlayerController p_Con = collision.gameObject.GetComponent<PlayerController>();

            Vector3 relativePos = this.transform.parent.position - player;
            relativePos.Normalize();

            Vector2 boopForce = new Vector3(relativePos.x * 3000, -500);
            Debug.Log(boopForce);

            rb.AddForce(-boopForce, ForceMode2D.Force);
        }
    }
}
