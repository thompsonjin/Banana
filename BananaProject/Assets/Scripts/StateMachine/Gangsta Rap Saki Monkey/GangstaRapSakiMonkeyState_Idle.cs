using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GangstaRapSakiMonkeyState_Idle : EnemyState
{
    public   GangstaRapSakiMonkeyState_Idle(string stateName)
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
 
        if (Vector2.Distance(  enemyControllor.transform  .position,enemyControllor.player.transform.position)>enemyControllor.attackRane)
        {
            stateMachine.SwitchState(typeof(GangstaRapSakiMonkeyState_Run));
            return;
        }
        
        enemyControllor.   SetMoveSpeed(Vector2.zero);
        if ( enemyControllor. player.transform.position.x> enemyControllor.  transform.position.x)
        {
            //ÏòÓÒ×·
            enemyControllor.    SetFaceDir(1); 
        }
        else
        {
            //Ïò×ó×·
            enemyControllor.    SetFaceDir(-1);
        }
        

  
    }
}
