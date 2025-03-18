using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBehaviour : MonoBehaviour
{
    private Vector3 scaleChange;
    private Vector3 correction;
    private RoboChimp r_Chi;

    // Start is called before the first frame update
    void Start()
    {
        r_Chi = this.transform.parent.parent.gameObject.GetComponent<RoboChimp>();
        scaleChange = new Vector3(0.04f, 0, 0);

        if (r_Chi.isFacingRight)
        {
            correction = new Vector3(0.02f, 0, 0);
        }
        else
        {
            correction = new Vector3(-0.02f, 0, 0);
        }           
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.localScale.x <= 20)
        {
            transform.localScale += scaleChange * 3;
            transform.position += correction * 3;
        }  
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Vector3 player = collision.gameObject.transform.position;
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            Vector3 pushForce = this.transform.parent.position - player;
            pushForce.Normalize();
            pushForce.x = pushForce.x * 1000;

            rb.AddForce(-pushForce, ForceMode2D.Force);
        }
    }
}
