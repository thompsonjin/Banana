using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSwitch : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerController>().GetGroundPound())
            {
                transform.parent.gameObject.GetComponent<RoboOrangutan>().Die();
            }          
        }
    }
}
