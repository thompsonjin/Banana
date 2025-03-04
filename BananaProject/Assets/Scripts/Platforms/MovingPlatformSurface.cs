using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformSurface : MonoBehaviour
{
    public MovingPlatform m_Plat;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            collision.transform.parent = this.transform;
            m_Plat.trigger = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
               collision.transform.parent = null;
        }
    }
}
