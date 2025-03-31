using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] float speed;
    private Vector3 target;
    public bool tracking;
    public bool random;

    // Start is called before the first frame update
    void Start()
    {
        if (random)
        {
            SetRandomTarget();
        }
        else
        {
            SetTargetPlayer();
        }         
    }

    // Update is called once per frame
    void Update()
    {
        if (tracking)
        {
            SetTargetPlayer();
        }
   
        rb.velocity = new Vector3(-target.x * speed, -target.y * speed, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage();
            Destroy(this.gameObject);
        }

        if(collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Throwable")
        {
            Destroy(this.gameObject);
        }
        
    }

    public void SetRandomTarget()
    {
        float x = Random.Range(-1, 2);
        float y = Random.Range(-1, 2);

        if(x == 0)
        {
            x += .5f;
        }
        if(y == 0)
        {
            y += .5f;
        }

        target = new Vector3(x,y,0);
    }

    public void SetTargetPlayer()
    {
        GameObject player = GameObject.Find("Player");

        if (player != null)
        {
            target = this.transform.position - player.transform.position;
            target.Normalize();
        }
    }
}
