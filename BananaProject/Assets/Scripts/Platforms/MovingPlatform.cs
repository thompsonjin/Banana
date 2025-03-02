using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] points;
    public GameObject movePoint;
    public GameObject platform;
    public float speed;
    public bool trigger;

    Transform target;
    int targetNum = 0;

    public GameObject platformSurface;

    private void Start()
    {
        target = points[targetNum];
    }
    private void Update()
    {
        if (trigger)
        {
            MovePlatform();
        }       
    }

    public void AddPoints()
    {
        Array.Resize(ref points, points.Length + 1);

        GameObject newObject = GameObject.Instantiate(movePoint, this.transform);
        points[points.Length - 1] = newObject.transform;

    }

    public void RemovePoint()
    {
        points[points.Length - 1].GetComponent<MovingPlatformPoint>().Delete();
        Array.Resize(ref points, points.Length - 1);
    }

    public void MovePlatform()
    {
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, target.position, speed * Time.deltaTime);

        if (platform.transform.position == target.position)
        {
            targetNum++;
            if(targetNum == points.Length)
            {
                targetNum = 0;
            }

            target = points[targetNum];
        }
    }
}
