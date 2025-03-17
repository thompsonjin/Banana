using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoliathOrangutanStateMachine : EnemyStateMachine
{
    public override void InitEnemyState()
    {
        base.InitEnemyState();
        states = new EnemyState[2]
        {
            new GoliathOrangutanState_Attack("GoliathOrangutanState_Attack"),
            new GoliathOrangutanState_Partrol("GoliathOrangutanState_Partrol"),

           
        };
        
        
    }

    private void Start()
    {

        SwitchOn(stateTable[typeof(GoliathOrangutanState_Partrol)]);
    }

    protected override void Update()
    {
        base.Update();

    }
}
