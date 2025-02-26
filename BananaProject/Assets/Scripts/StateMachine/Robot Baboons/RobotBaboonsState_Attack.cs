using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBaboonsState_Attack : EnemyState
{
    private float attackTimer;
    public   RobotBaboonsState_Attack(string stateName)
    {
        statename = stateName;
        base.Init();
    }

    public override void Enter()
    {
        stateMachine.currentStateName = statename;

        stateStartTime = Time.time;
        attackTimer = 10f;
    }

    public override void Exit()
    {
        base.Exit();
   
    }



    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (     Vector2.Distance( enemyControllor. transform.position,  enemyControllor. player.transform.position)>  enemyControllor. attackRane)
        {

            stateMachine.SwitchState(typeof(RobotBaboonsState_Run));

            return;
        }

       enemyControllor. SetMoveSpeed(new Vector2(0, 0));
        if ( enemyControllor.  transform.position.x< enemyControllor.   player.transform.position.x)
        {
            //player on right
            enemyControllor.    SetFaceDir(1);
        }
        else
        {
            //player on left
            enemyControllor.    SetFaceDir(-1);
        }

        attackTimer += Time.deltaTime;
        if (attackTimer>1/ enemyControllor. attackSpeed)
        {
           
            //animation
            animator.Play(statehash);
            attackTimer = 0;
        }
  
    }
}
