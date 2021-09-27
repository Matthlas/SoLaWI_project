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


    public override void OnEnable()
    {
        guaranteeAnimatorAndNavMesh();
        wanderState = this.GetComponent<WanderState>();
        currentState = wanderState;
        
    }
}
