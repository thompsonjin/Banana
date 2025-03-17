using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProboscisMonkeStateMachine : EnemyStateMachine
{
    public override void InitEnemyState()
    {
        base.InitEnemyState();
        states = new EnemyState[2]
        {
            new LaserProboscisMonkeSatte_Attack("LaserProboscisMonkeSatte_Attack"),
            new LaserProboscisMonkeSatte_Partrol("LaserProboscisMonkeSatte_Partrol"),
           
        };
        
        
    }
    private void Start()
    {

        SwitchOn(stateTable[typeof(LaserProboscisMonkeSatte_Partrol)]);
    }

    protected override void Update()
    {
        base.Update();
    }
}
