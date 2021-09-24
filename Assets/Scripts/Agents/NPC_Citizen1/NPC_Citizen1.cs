using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

[RequireComponent(typeof(IdleBasicState))]

public class NPC_Citizen1 : NPC_BaseClass
{
    
    
    [HideInInspector] public IdleBasicState idleState;
    public PerformTaskState performTaskState;
    
    [HideInInspector] private string[] workingAnimations = {"attack_1"};


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
        // Create a Random object  
        Random rand = new Random();  
        // Generate a random index less than the size of the array.  
        int index = rand.Next(workingAnimations.Length); 
        mAnimator.SetTrigger(workingAnimations[index]);
    }

    public void animateTalking()
    {
        mAnimator.SetTrigger("tr_drop");
    }
}