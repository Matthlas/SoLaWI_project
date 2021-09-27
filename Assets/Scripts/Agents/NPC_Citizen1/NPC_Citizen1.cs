using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;
using RandomUnity = UnityEngine.Random;

[RequireComponent(typeof(IdleBasicState))]

public class NPC_Citizen1 : NPC_BaseClass
{
    
    
    [HideInInspector] public IdleBasicState idleState;

    //Animation variables
    [HideInInspector] private string[] workingAnimations = {"attack_1"};
    [HideInInspector] private string[] talkingAnimations = {"wave", "tr_drop"};
    private bool aboutToTalk = false; 


    public override void OnEnable()
    {
        idleState = this.GetComponent<IdleBasicState>();
        currentState = idleState;
        
        guaranteeAnimatorAndNavMesh();
    }


    public override void animateWalking()
    {
        mAnimator.SetBool("walk", IsNavMeshMoving);
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
        if (!aboutToTalk)
        {
            Invoke("makeTalkingMotion", RandomUnity.Range(0f, 2f));
            aboutToTalk = true;
        }
        
    }

    public void stopAllTalking()
    {
        CancelInvoke("makeTalkingMotion");
    }

    private void makeTalkingMotion()
    {
        // // Create a Random object  
        // Random rand = new Random();  
        // // Generate a random index less than the size of the array.  
        // int index = rand.Next(talkingAnimations.Length); 
        // mAnimator.SetTrigger(talkingAnimations[index]);
        mAnimator.SetTrigger("tr_drop");
        aboutToTalk = false;
    }
    

    public void animateWaving()
    {
        mAnimator.SetTrigger("wave");
    }
}