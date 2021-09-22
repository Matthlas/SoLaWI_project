using UnityEngine;
using UnityEngine.AI;

public class FollowState : INPCState
{
    public INPCState DoState(NPC_Sheep npc)
    {
        //Guarantee NavMeshAgent
        if (npc.navAgent == null)
            npc.navAgent = npc.GetComponent<NavMeshAgent>();

        //Execute following specific actions
        DoFollow(npc);
        
        //Conditions for state transitions
        if (npc.CloseToAvoiding())
            return npc.avoidState;
        else if (!npc.FarFromFollowing())
            //Possibly stop agent
            return npc.wanderState;
        else
            return npc.followState;
    }

    private void DoFollow(NPC_Sheep npc)
    {
        if (!npc.chillin) //Slighty delay the following for more organic aesthetics
        {
            //Calculate direction vector to following and set new destination
            Vector3 dirToFollowing = npc.transform.position - npc.Following.transform.position;
            Vector3 newPos = npc.transform.position - dirToFollowing;

            if (npc.navAgent.destination != newPos)
                npc.navAgent.SetDestination(newPos);
        }
    }
    
}