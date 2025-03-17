using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBaboonsStateMaschine : EnemyStateMachine
{

    public override void InitEnemyState()
    {
        base.InitEnemyState();
        states = new EnemyState[3]
        {
            new RobotBaboonsState_Attack("RobotBaboonsState_Attack"),
            new RobotBaboonsState_Run("RobotBaboonsState_Run"),
            new RobotBaboonsState_Patrol("RobotBaboonsState_Patrol")
        };
        
        
    }
 
    private void Start()
    {

        SwitchOn(stateTable[typeof(RobotBaboonsState_Patrol)]);
    }

    protected override void Update()
    {
    base.Update();
    

    }
}
