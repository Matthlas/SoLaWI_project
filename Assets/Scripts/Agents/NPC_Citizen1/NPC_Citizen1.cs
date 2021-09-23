using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(IdleBasicState))]

public class NPC_Citizen1 : NPC_BaseClass
{
    
    [HideInInspector] public IdleBasicState idleState;
    public PerformTaskState performTaskState;


    public override void OnEnable()
    {
        idleState = this.GetComponent<IdleBasicState>();
        performTaskState = this.GetComponent<PerformTaskState>();
        
        currentState = performTaskState;
        
        guaranteeAnimatorAndNavMesh();
    }

    public override void animateWalking()
    {
        mAnimator.SetBool("run", IsNavMeshMoving);
    }

    public void animateWorking()
    {
        mAnimator.SetTrigger("attack_1");
    }
}