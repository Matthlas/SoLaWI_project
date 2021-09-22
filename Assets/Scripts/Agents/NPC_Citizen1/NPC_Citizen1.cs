using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(WanderState_Sheep))]
[RequireComponent(typeof(AvoidState_Sheep))]
[RequireComponent(typeof(FollowState_Sheep))]

public class NPC_Citizen1 : NPC_BaseClass
{
    
    [HideInInspector] public WanderState_Sheep wanderState;
    [HideInInspector] public FollowState_Sheep followState;
    [HideInInspector] public AvoidState_Sheep avoidState;
    

    public override void OnEnable()
    {
        wanderState = this.GetComponent<WanderState_Sheep>();
        followState = this.GetComponent<FollowState_Sheep>();
        avoidState = this.GetComponent<AvoidState_Sheep>();
        currentState = wanderState;
        if (mAnimator == null)
            mAnimator = GetComponent<Animator>();
        if (navAgent == null)
            navAgent = GetComponent<NavMeshAgent>();
    }

    public override void animate()
    {
        mAnimator.SetBool("run", IsNavMeshMoving);
    }
}