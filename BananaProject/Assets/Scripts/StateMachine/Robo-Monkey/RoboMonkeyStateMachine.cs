using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboMonkeyStateMachine : EnemyStateMachine  
{
    public override void InitEnemyState()
    {
        base.InitEnemyState();
        states = new EnemyState[3]
        {
            new RobotMonkey_Attack("RobotMonkey_Attack"),
            new RobotMonkey_Patrol("RobotMonkey_Patrol"),
            new RobotMonkey_Run("RobotMonkey_Run")
           
        };
        
        
    }
    private void Start()
    {

        SwitchOn(stateTable[typeof(RobotMonkey_Patrol)]);
    }

    protected override void Update()
    {
        base.Update();
     

    }
}
