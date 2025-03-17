using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGunChimpState_Attack : EnemyState
{

    public   WaterGunChimpState_Attack(string stateName)
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
        enemyControllor. SetMoveSpeed(new Vector2(0, 0));
        if (isAnimationFinished)
        {
            stateMachine.SwitchState(typeof(WaterGunChimpState_Partrol));
        }
    }
}
