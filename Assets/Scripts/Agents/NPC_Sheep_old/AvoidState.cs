using UnityEngine;
using UnityEngine.AI;

public class AvoidState : INPCState
{
    public INPCState DoState(NPC_Sheep npc)
    {
        //Guarantee NavMeshAgent
        if (npc.navAgent == null)
            npc.navAgent = npc.GetComponent<NavMeshAgent>();

        //Execute avoiding specific actions
        DoAvoid(npc);
        
        //Conditions for state transitions
        if (npc.CloseToAvoiding())
            return npc.avoidState;
        else
            return npc.wanderState;
    }

    private void DoAvoid(NPC_Sheep npc)
    {
        //Calculate direction vector to avoiding and set new destination
        Vector3 dirToAvoiding = npc.transform.position - npc.Avoiding.transform.position;
        Vector3 newPos = npc.transform.position + dirToAvoiding;
        
        if (npc.navAgent.destination != newPos)
            npc.navAgent.SetDestination(newPos);
        
    }
}