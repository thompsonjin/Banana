using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPush : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 player = collision.gameObject.transform.position;
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            Vector3 pushForce = this.transform.parent.parent.position - player;
            pushForce.Normalize();
            pushForce.x = pushForce.x * 1000;

            rb.AddForce(-pushForce, ForceMode2D.Force);
        }
    }
}
