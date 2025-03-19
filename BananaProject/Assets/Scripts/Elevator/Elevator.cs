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

    [Header("Outside Graphic")]
    public GameObject outside;
    public Transform startPos;
    public Transform endPos;

    // Start is called before the first frame update
    void Start()
    {
        timer = elevatorTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Destroy(door);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                timer = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            buttonPrompt.SetActive(true);
        }
           
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            buttonPrompt.SetActive(false);
        }
    }
}
