using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSwitch : MonoBehaviour
{
    public bool orangutan;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerController>().GetGroundPound())
            {
                if (orangutan)
                {
                    transform.parent.gameObject.GetComponent<RoboOrangutan>().Die();
                }
                else
                {
                    GameObject.FindWithTag("BFBG").GetComponent<BFBGManager>().NextPhase();
                    Destroy(transform.gameObject);
                }
            }          
        }
    }
}
