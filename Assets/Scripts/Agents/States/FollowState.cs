using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AvoidState))]
[RequireComponent(typeof(WanderState))]

public class FollowState : NPCState
{
    [HideInInspector] public NPC_BaseClass npc;
    [HideInInspector] public WanderState wanderState;
    [HideInInspector] public FollowState followState;
    [HideInInspector] public AvoidState avoidState;
    
    // Following variables
    public GameObject Following;
    public float FollowDistance = 5.0f;
    
    private void OnEnable()
    {
        npc = this.GetComponent<NPC_BaseClass>();
        wanderState = this.GetComponent<WanderState>();
        followState = this;
        avoidState = this.GetComponent<AvoidState>();
    }

    public override void ExecuteStateAction()
    {
        if (!wanderState.chillin) //Slighty delay the following for more organic aesthetics
        {
            //Calculate direction vector to following and set new destination
            Vector3 dirToFollowing = npc.transform.position - followState.Following.transform.position;
            Vector3 newPos = npc.transform.position - dirToFollowing;

            if (npc.navAgent.destination != newPos)
                npc.navAgent.SetDestination(newPos);
        }
    }

    public override NPCState TransitionToState()
    {
        if (avoidState.CloseToAvoiding())
            return avoidState;
        else if (!followState.FarFromFollowing())
            //Possibly stop agent
            return wanderState;
        else
            return followState;
    }
    
    public bool FarFromFollowing()
    {
        return Vector3.Distance(transform.position, Following.transform.position) > FollowDistance;
    }
}