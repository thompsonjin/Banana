using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoliathOrangutanState_Attack : EnemyState
{
    private float attackTimer = 0;
    public   GoliathOrangutanState_Attack(string stateName)
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
        
        if ((enemyControllor.transform.localScale.x>0&& enemyControllor.player.position.x>enemyControllor.transform.position.x)||(enemyControllor.transform.localScale.x<0&& enemyControllor.player.position.x<enemyControllor.transform.position.x)   )
        {
            if (  Vector2.Distance(enemyControllor.transform.position,enemyControllor.player.transform.position)>enemyControllor.attackRane)
            {
                stateMachine.SwitchState(typeof(GoliathOrangutanState_Partrol));
                return;
            }
        }
        else
        {
            stateMachine.SwitchState(typeof(GoliathOrangutanState_Partrol));
            return;
        }

        enemyControllor. SetMoveSpeed(new Vector2(0, 0));

        attackTimer += Time.deltaTime;
        if (attackTimer>1/ enemyControllor. attackSpeed)
        {
            //·Å¶¯»­
            animator.Play(statehash);
            attackTimer = 0;
        }
    }
}
