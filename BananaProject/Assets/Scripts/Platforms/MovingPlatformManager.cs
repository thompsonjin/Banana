using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformManager : MonoBehaviour
{
    public Transform[] points;
    public GameObject movePoint;
    public float speed;

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
}
