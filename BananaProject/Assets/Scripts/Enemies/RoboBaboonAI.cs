using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStates
{
    patrol, attack
}



public class RoboBaboonAI : MonoBehaviour
{

    [Header("Partol Speed")]
    public float partolSpeed;

    public List<GameObject> patrolPoints = new List<GameObject>();
    public int currentPartrolPointIndex = 0;

    private void Start()
    {
        currentState = EnemyStates.patrol;

    }

    public EnemyStates currentState;


    private void Update()
    {
        if (currentState == EnemyStates.patrol)
        {
            if (currentPartrolPointIndex == 1)
            {
                SetMoveSpeed(new Vector2(1, 0) * partolSpeed);
                SetFaceDir(1);
            }
            else if (currentPartrolPointIndex == 0)
            {
                SetMoveSpeed(new Vector2(-1, 0) * partolSpeed);
                SetFaceDir(-1);
            }

            if (Mathf.Abs(transform.position.x - patrolPoints[currentPartrolPointIndex].transform.position.x) < 0.1f)
            {
                //Arrive PartrolPoint
                currentPartrolPointIndex = 1 - currentPartrolPointIndex;
                return;
            }
        }
        else if (currentState == EnemyStates.attack)
        {

        }

    }

    void SetFaceDir(int dir)
    {
        if(dir==1)
        {
            transform.localScale = new Vector3(1,1,1);

        }
        else if(dir==-1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }
    void SetMoveSpeed(Vector2 speed)
    {
        GetComponent<Rigidbody2D>().velocity = speed;

    }



}
