using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLine : MonoBehaviour
{
    public float flySpeed;
    public bool flyRight;
    private Vector2 Speed;
    // Start is called before the first frame update
    void Start()
    {
        if (flyRight)
        {
            Speed = Vector2.right * flySpeed;
        }
        else 
        {
            Speed = Vector2.left * flySpeed;
        }

        GetComponent<Rigidbody2D>().velocity = Speed;
        Destroy(gameObject,10f);
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
