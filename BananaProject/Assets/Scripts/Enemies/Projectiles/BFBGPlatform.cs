using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFBGPlatform : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    private float speed;
    BFBGManager b_Man;

    // Start is called before the first frame update
    void Start()
    {
        b_Man = GameObject.FindWithTag("BFBG").GetComponent<BFBGManager>();
        speed = b_Man.speed;
        rb.velocity = new Vector2(-1 * speed, 0);
        //rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Floor")
        {
            Destroy(transform.gameObject);
        }
    }
}
