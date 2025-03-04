using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFBGPlatform : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-1, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            Destroy(transform.gameObject);
        }
    }
}
