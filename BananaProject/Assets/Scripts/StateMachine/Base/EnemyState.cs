using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : IState
{
    protected float stateStartTime;
    protected string statename;
    protected int statehash;
    protected Animator animator;
    protected EnemyStateMachine stateMachine;
    protected EnemyController enemyControllor;


    
    
    protected bool isAnimationFinished => stateduring >= animator.GetCurrentAnimatorStateInfo(0).length;
    protected float stateduring => Time.time - stateStartTime;
    
    public void Initialize(Animator animator, EnemyController enemyControllor, EnemyStateMachine stateMachine)
    {
        this.enemyControllor = enemyControllor;
        this.animator = animator;
        this.stateMachine = stateMachine;
    }
    //¹¹Ôìº¯Êý
    public void Init()
    {
        statehash = Animator.StringToHash(statename);
     
    }


    public virtual void Enter()
    {
        stateMachine.currentStateName = statename;
        if (animator != null)
        {
            animator.Play(statehash);
        }
        stateStartTime = Time.time;
    }

    public virtual void Exit()
    {
    }

    public virtual void LogicUpdate()
    {
       
    }


}
