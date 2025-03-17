using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GangstaRapSakiMonkeyStateMachine : EnemyStateMachine
{
    public override void InitEnemyState()
    {
        base.InitEnemyState();
        states = new EnemyState[4]
        {
            new GangstaRapSakiMonkeyState_Attack("GangstaRapSakiMonkeyState_Attack"),
            new GangstaRapSakiMonkeyState_Partrol("GangstaRapSakiMonkeyState_Partrol"),
            new GangstaRapSakiMonkeyState_Run("GangstaRapSakiMonkeyState_Run"),
            new GangstaRapSakiMonkeyState_Idle("GangstaRapSakiMonkeyState_Idle")
           
        };
        
        
    }
    private void Start()
    {

        SwitchOn(stateTable[typeof(GangstaRapSakiMonkeyState_Partrol)]);
    }

    private float timer;
    protected override void Update()
    {
        base.Update();
        timer += Time.deltaTime;
        if (timer>5f)
        {
            SwitchOn(stateTable[typeof(GangstaRapSakiMonkeyState_Attack)]);
            timer = 0;
        }
    }
}
