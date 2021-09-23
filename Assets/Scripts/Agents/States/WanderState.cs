using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[RequireComponent(typeof(FollowState))]

public class WanderState : IdleBasicState
{
    
    [HideInInspector] public WanderState wanderState;
    [HideInInspector] public FollowState followState;


    //Wandering
    [SerializeField] public float ExplorationRadius = 2.0f;
    [SerializeField] public float chillTime = 5.0f;
    public bool chillin = false;
    
    private void OnEnable()
    {
        npc = this.GetComponent<NPC_BaseClass>();
        wanderState = this;
        followState = this.GetComponent<FollowState>();
        avoidState = this.GetComponent<AvoidState>();
        ChillABit();
    }

    public override void ExecuteStateAction()
    {
        //if close to current target chill and then choose next location
        if (npc.navAgent.remainingDistance < followState.FollowDistance/2)
        {
            if (!chillin)
            {
                SetNewExplorationPoint();
                //Randomized chill time for more dynamic behaviour
                ChillABit();
            }
        }
    }

    public override NPCState TransitionToState()
    {
        if (avoidState.CloseToAvoiding())
            return avoidState;
        else if (followState.FarFromFollowing())
            return followState;
        else
            return wanderState;
    }
    

    public void SetNewExplorationPoint()
    {
        Vector3 explorePosition = new Vector3(Random.Range(-wanderState.ExplorationRadius, wanderState.ExplorationRadius), Random.Range(-wanderState.ExplorationRadius, wanderState.ExplorationRadius), 1);
        npc.nextLocation = npc.transform.position + explorePosition;

        if (NavMesh.SamplePosition(npc.nextLocation, out NavMeshHit hit, 5f, NavMesh.AllAreas))
        {
            npc.nextLocation = hit.position;
            npc.navAgent.SetDestination(npc.nextLocation);
        }
    }
    

    public void ChillABit()
    {
        if (!chillin)
        {
            chillin = true;
            Invoke("StopChilling", chillTime + Random.Range(-chillTime/2, chillTime));
        }
    }
    
    public void StopChilling()
    {
        chillin = false;
    }
}