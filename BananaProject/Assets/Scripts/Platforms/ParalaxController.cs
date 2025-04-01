using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ParalaxController : MonoBehaviour
{
    private float startPos;
    private float spriteLength;
    [SerializeField] private float parallaxAmount;
    public Camera mainCam;



    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;

        spriteLength = GetComponent<TilemapRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camPos = mainCam.transform.position;
        float temp = camPos.x * (1 - parallaxAmount);
        float dist = camPos.x * parallaxAmount;

        Vector3 newPos = new Vector3(startPos + dist, transform.position.y, transform.position.z);
        transform.position = newPos;
    }
}
