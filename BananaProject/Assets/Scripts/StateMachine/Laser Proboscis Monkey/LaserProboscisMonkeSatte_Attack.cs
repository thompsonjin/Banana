using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProboscisMonkeSatte_Attack : EnemyState
{
    public   LaserProboscisMonkeSatte_Attack(string stateName)
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
            stateMachine.SwitchState(typeof(LaserProboscisMonkeSatte_Partrol));
        }
    }
}
