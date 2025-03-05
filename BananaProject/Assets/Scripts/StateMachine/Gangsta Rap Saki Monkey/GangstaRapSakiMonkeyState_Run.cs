using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GangstaRapSakiMonkeyState_Run : EnemyState
{
    public   GangstaRapSakiMonkeyState_Run(string stateName)
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
 
        if (Vector2.Distance(  enemyControllor.transform  .position,enemyControllor.player.transform.position)>enemyControllor.radorRange)
        {
            stateMachine.SwitchState(typeof(GangstaRapSakiMonkeyState_Partrol));
            return;
        }
        if (Vector2.Distance(enemyControllor.transform.position,enemyControllor.player.transform.position)<enemyControllor.attackRane)
        {
            stateMachine.SwitchState(typeof(GangstaRapSakiMonkeyState_Idle));
            return;
        }
        if ( enemyControllor. player.transform.position.x> enemyControllor.  transform.position.x)
        {
            //ÏòÓÒ×·
                    
            enemyControllor.   SetMoveSpeed(new Vector2(1,0)*enemyControllor.   runSpeed);
            enemyControllor.    SetFaceDir(1);
                
        }
        else
        {
            //Ïò×ó×·
            enemyControllor. SetMoveSpeed(new Vector2(-1,0)*enemyControllor.runSpeed);
            enemyControllor.    SetFaceDir(-1);
        }
            
        if ( enemyControllor. transform.position.x>=enemyControllor.patrolPoints[1].transform.position.x)
        {
            enemyControllor. transform.position = new Vector3(enemyControllor.patrolPoints[1].transform.position.x,enemyControllor.transform.position.y,enemyControllor.transform.position.z);
        }
        else  if( enemyControllor.transform.position.x<=enemyControllor.patrolPoints[0].transform.position.x  )
        {
            enemyControllor. transform.position = new Vector3(enemyControllor.patrolPoints[0].transform.position.x,enemyControllor.transform.position.y,enemyControllor.transform.position.z);
        }
  
    }
}
