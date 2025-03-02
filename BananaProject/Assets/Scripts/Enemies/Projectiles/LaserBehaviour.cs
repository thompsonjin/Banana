using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] float speed;
    private Vector3 target;
    private GameObject player;




    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        target = this.transform.position - player.transform.position;
        target.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
         rb.velocity = new Vector3(-target.x * speed, -target.y * speed, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage();
            Destroy(this.gameObject);
        }

        if(collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
        
    }
}
