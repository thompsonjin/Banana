using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] points;
    public GameObject movePoint;
    public float speed;
    [SerializeField] public LayerMask platformLayer;

    Transform target;
    int targetNum = 0;

    private void Start()
    {
        target = points[targetNum];
    }
    private void Update()
    {
        MovePlatform();
    }

    public void AddPoints()
    {
        Array.Resize(ref points, points.Length + 1);

        GameObject newObject = GameObject.Instantiate(movePoint);
        newObject.transform.SetParent(this.transform);
        points[points.Length - 1] = newObject.transform;

    }

    public void RemovePoint()
    {
        points[points.Length - 1].GetComponent<MovingPlatformPoint>().Delete();
        Array.Resize(ref points, points.Length - 1);
    }

    public void MovePlatform()
    {

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if(transform.position == Physics2D.OverlapCircleAll(target.position, 1, platformLayer))
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
