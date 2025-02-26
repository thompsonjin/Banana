using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMonkey_Attack : EnemyState
{
    private float attackTimer;
    public   RobotMonkey_Attack(string stateName)
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
 
        if ( Vector2.Distance( enemyControllor. transform.position,  enemyControllor. player.transform.position)>  enemyControllor. attackRane)
        {

            stateMachine.SwitchState(typeof(RobotMonkey_Run));

            return;
        }

        enemyControllor. SetMoveSpeed(new Vector2(0, 0));
        if ( enemyControllor.  transform.position.x< enemyControllor.   player.transform.position.x)
        {
            //玩家在右侧
            enemyControllor.    SetFaceDir(1);
        }
        else
        {
            //玩家在左侧
            enemyControllor.    SetFaceDir(-1);
        }
        attackTimer += Time.deltaTime;
        if (attackTimer>1/ enemyControllor. attackSpeed)
        {
            //放动画
            
            animator.Play(statehash);
            attackTimer = 0;
        }
  
    }
}
