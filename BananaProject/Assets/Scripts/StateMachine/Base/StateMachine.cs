using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected IState currentState;
    protected Dictionary<System.Type, IState> stateTable;
    protected virtual void Update()
    {
        currentState.LogicUpdate();
    }
    
    public void SwitchState(System.Type newStateType)
    {
        currentState.Exit();
        SwitchOn(stateTable[newStateType]);
    }
    
    
    protected void SwitchOn(IState newState)
    {
        currentState = newState;
        currentState.Enter();
    }




}
