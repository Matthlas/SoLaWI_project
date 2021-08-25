using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class WanderState : INPCState
{
    public INPCState DoState(NPC_Sheep npc)
    {
        //Guarantee NavMeshAgent
        if (npc.navAgent == null)
        {
            npc.nextLocation = npc.transform.position;
            npc.navAgent = npc.GetComponent<NavMeshAgent>();
        }

        //Execute wandering specific actions
        DoWander(npc);
        
        //Conditions for state transitions
        if (npc.CloseToAvoiding())
            return npc.avoidState;
        else if (npc.FarFromFollowing())
            return npc.followState;
        else
            return npc.wanderState;
    }

    public void SetNewExplorationPoint(NPC_Sheep npc)
    {
        Vector3 explorePosition = new Vector3(Random.Range(-npc.ExplorationRadius, npc.ExplorationRadius), Random.Range(-npc.ExplorationRadius, npc.ExplorationRadius), 1);
        npc.nextLocation = npc.transform.position + explorePosition;

        if (NavMesh.SamplePosition(npc.nextLocation, out NavMeshHit hit, 5f, NavMesh.AllAreas))
        {
            npc.nextLocation = hit.position;
            npc.navAgent.SetDestination(npc.nextLocation);
        }
    }

    private void DoWander(NPC_Sheep npc)
    {
        //if close to current target chill and then choose next location
        if (npc.navAgent.remainingDistance < npc.FollowDistance/2)
        {
            if (!npc.chillin)
            {
                SetNewExplorationPoint(npc);
                //Randomized chill time for more dynamic behaviour
                npc.ChillABit();
            }
            
        }
    }
}
