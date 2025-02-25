using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaveBehaviour : MonoBehaviour
{
    private Vector3 scaleChange;
    private Vector3 positionChange;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 player = collision.gameObject.transform.position;
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            Vector3 relativePos = this.transform.parent.position - player;
            relativePos.Normalize();

            Vector3 boopForce = new Vector3(relativePos.x * 10000, -20, 0);

            rb.AddForce(-boopForce, ForceMode2D.Impulse);
        }
    }
}
