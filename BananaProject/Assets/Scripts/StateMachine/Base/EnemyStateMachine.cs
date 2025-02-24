using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent( typeof( EnemyController))]
public class EnemyStateMachine : StateMachine
{
    public EnemyState[] states;
    public string currentStateName;
    Animator animator;
    EnemyController enemyControllor;

    public void Awake()
    {
        InitEnemyState();
        animator = GetComponentInChildren<Animator>();
        enemyControllor = GetComponent<EnemyController>();
        stateTable = new Dictionary<System.Type, IState>(states.Length);
        for (int i = 0; i < states.Length; i++)
        {
            states[i].Initialize(animator, enemyControllor, this);
            stateTable.Add(states[i].GetType(), states[i]);
        }
    }
    
    
    
    public virtual void InitEnemyState()
    {
     
    }
}
