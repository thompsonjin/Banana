using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProboscisMonkeSatte_Partrol : EnemyState
{
    public   LaserProboscisMonkeSatte_Partrol(string stateName)
    {
        statename = stateName;
        base.Init();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
   
    }



    public override void LogicUpdate()
    {
        base.LogicUpdate();
 
        if (stateduring>5f)
        {
            stateMachine.SwitchState(typeof(LaserProboscisMonkeSatte_Attack));
            return;
        }
        
        if (enemyControllor.patrolPointIndex==1)
        {
            enemyControllor. SetMoveSpeed(new Vector2(1,0)* enemyControllor.    partolSpeed);
            enemyControllor.    SetFaceDir(1);
        }
        else  if( enemyControllor.patrolPointIndex==0  )
        {
            enemyControllor. SetMoveSpeed(new Vector2(-1,0)*enemyControllor.partolSpeed);
            enemyControllor.    SetFaceDir(-1);
        }

        if (  Mathf.Abs( enemyControllor. transform.position.x-   enemyControllor. patrolPoints[enemyControllor.patrolPointIndex].transform.position.x)   <0.2f)
        {
            Debug.Log(1);
            //到达巡逻点了
            enemyControllor.patrolPointIndex = 1 - enemyControllor.patrolPointIndex;
            return;
        }
  
    }
}
