using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(WanderState))]

public class FollowState : NPCState
{
    [HideInInspector] private NPC_BaseClass npc;
    [HideInInspector] private WanderState wanderState;
    [HideInInspector] private AvoidState avoidState;
    
    // Following variables
    [SerializeField] private GameObject Following;
    [SerializeField] private float FollowDistance = 5.0f;
    
    private void OnEnable()
    {
        npc = this.GetComponent<NPC_BaseClass>();
        wanderState = this.GetComponent<WanderState>();
        avoidState = this.GetComponent<AvoidState>();
    }

    public override void ExecuteStateAction()
    {
        if (!wanderState.chillin) //Slighty delay the following for more organic aesthetics
        {
            //Calculate direction vector to following and set new destination
            Vector3 dirToFollowing = npc.transform.position - Following.transform.position;
            Vector3 newPos = npc.transform.position - dirToFollowing;

            if (npc.navAgent.destination != newPos)
                npc.navAgent.SetDestination(newPos);
        }
    }

    public override NPCState TransitionToState()
    {
        if (avoidState != null)
        {
            if (avoidState.CloseToAvoiding())
                return avoidState;
        }
        if (!FarFromFollowing())
            return wanderState;
        else
            return this;
    }
    
    
    // Checks whether the npc is far from the object to follow
    public bool FarFromFollowing()
    {
        if (Following != null)
            return Vector3.Distance(transform.position, Following.transform.position) > FollowDistance;
        else
            return false;
    }
}