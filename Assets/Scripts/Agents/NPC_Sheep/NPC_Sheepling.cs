using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(WanderState))]
[RequireComponent(typeof(AvoidState))]
[RequireComponent(typeof(FollowState))]

public class NPC_Sheepling : NPC_BaseClass
{
    
    [HideInInspector] public WanderState wanderState;
    [HideInInspector] public FollowState followState;
    [HideInInspector] public AvoidState avoidState;
    

    public override void OnEnable()
    {
        wanderState = this.GetComponent<WanderState>();
        followState = this.GetComponent<FollowState>();
        avoidState = this.GetComponent<AvoidState>();
        currentState = wanderState;
        if (mAnimator == null)
            mAnimator = GetComponent<Animator>();
        if (navAgent == null)
            navAgent = GetComponent<NavMeshAgent>();
    }
}
