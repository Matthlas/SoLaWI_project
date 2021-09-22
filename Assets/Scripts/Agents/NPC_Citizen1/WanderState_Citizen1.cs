using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class WanderState_Citizen1 : INPCState_Citizen1
{
    public INPCState_Citizen1 DoState(NPC_Citizen1 npc)
    {
        //Guarantee NavMeshAgent
        if (npc.navAgent == null)
        {
            npc.nextLocation = npc.transform.position;
            npc.navAgent = npc.GetComponent<NavMeshAgent>();
        }

        //Execute wandering specific actions
        DoWander(npc);
        
        // //Conditions for state transitions
        // if (npc.CloseToAvoiding())
        //     return npc.avoidState;
        // else if (npc.FarFromFollowing())
        //     return npc.followState;
        // else
            return npc.wanderState;
    }

    public void SetNewExplorationPoint(NPC_Citizen1 npc)
    {
        Vector3 explorePosition = new Vector3(Random.Range(-npc.ExplorationRadius, npc.ExplorationRadius), Random.Range(-npc.ExplorationRadius, npc.ExplorationRadius), 1);
        npc.nextLocation = npc.transform.position + explorePosition;

        if (NavMesh.SamplePosition(npc.nextLocation, out NavMeshHit hit, 5f, NavMesh.AllAreas))
        {
            npc.nextLocation = hit.position;
            npc.navAgent.SetDestination(npc.nextLocation);
        }
    }

    private void DoWander(NPC_Citizen1 npc)
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
