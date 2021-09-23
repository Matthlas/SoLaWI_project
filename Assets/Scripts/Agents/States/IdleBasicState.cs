using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AvoidState))]

public class IdleBasicState : NPCState
{
    
    [HideInInspector] public NPC_BaseClass npc;
    [HideInInspector] public IdleBasicState idleState;
    [HideInInspector] public AvoidState avoidState;


    protected virtual void OnEnable()
    {
        npc = this.GetComponent<NPC_BaseClass>();
        idleState = this;
        avoidState = this.GetComponent<AvoidState>();
    }

    public override void ExecuteStateAction()
    {

    }

    public override NPCState TransitionToState()
    {
        if (avoidState.CloseToAvoiding())
            return avoidState;
        else
            return this;
    }
}