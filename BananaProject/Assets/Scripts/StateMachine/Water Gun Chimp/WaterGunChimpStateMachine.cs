using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGunChimpStateMachine : EnemyStateMachine
{ 
    public override void InitEnemyState()
    {
        base.InitEnemyState();
        states = new EnemyState[2]
        {
            new WaterGunChimpState_Attack("WaterGunChimpState_Attack"),
            new WaterGunChimpState_Partrol   ("WaterGunChimpState_Partrol"),
        };
        
        
    }
    private void Start()
    {

        SwitchOn(stateTable[typeof(WaterGunChimpState_Partrol)]);
    }

    protected override void Update()
    {
        base.Update();
     

    }
}
