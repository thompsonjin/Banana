using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class Elevator : MonoBehaviour
{
    public float elevatorTime;
    private float timer;
    public GameObject door;
    public GameObject buttonPrompt;
    bool canSkip;

    [Header("Outside Graphic")]
    public GameObject outside;
    public Transform endPos;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        timer = elevatorTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(door != null)
        {
            if (timer <= 0)
            {
                Destroy(door);
            }
        }

        if(outside.transform.position.y > endPos.position.y)
        {
            outside.transform.Translate(new Vector3(0, -1 * speed, 0));
        }

        if (Input.GetKeyDown(KeyCode.E) && canSkip)
        {
            Destroy(door);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            buttonPrompt.SetActive(true);
            canSkip = true;
        }
           
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            buttonPrompt.SetActive(false);
            canSkip = false;
        }
    }
}
