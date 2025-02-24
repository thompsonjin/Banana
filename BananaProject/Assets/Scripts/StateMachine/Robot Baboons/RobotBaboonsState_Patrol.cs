using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBaboonsState_Patrol : EnemyState
{
      public   RobotBaboonsState_Patrol(string stateName)
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
        if (Vector2.Distance(enemyControllor.transform.position,enemyControllor.player.transform.position)<enemyControllor.radorRange)
        {
            stateMachine.SwitchState(typeof(RobotBaboonsState_Run));
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

        if (  Mathf.Abs( enemyControllor. transform.position.x-   enemyControllor. patrolPoints[enemyControllor.patrolPointIndex].transform.position.x)   <0.1f)
        {
            //到达巡逻点了
            enemyControllor.patrolPointIndex = 1 - enemyControllor.patrolPointIndex;
            return;
        }
  
    }


}
