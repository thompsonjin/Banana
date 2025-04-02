using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFBGLaserBehaviour : MonoBehaviour
{
    bool stop;
    BFBGManager b_Man;

    [SerializeField]float timer;

    // Start is called before the first frame update
    void Start()
    {
        b_Man = GameObject.FindWithTag("BFBG").GetComponent<BFBGManager>();
    }

    public void Fire(Vector3 scale, Vector3 cor, float range)
    {
        if (transform.localScale.x > -range && !stop)
        {
            transform.localScale -= scale * 3;
            transform.position -= cor * 3;
        }

        if(transform.localScale.x <= -range)
        {
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                b_Man.endLaser = true;
            }        
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage();
        }

        if(collision.gameObject.tag == "Wall")
        {
            stop = true;
        }
    }


}
