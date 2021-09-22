using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AvoidState_Sheep))]
[RequireComponent(typeof(WanderState_Sheep))]

public class FollowState_Sheep : NPCState
{
    [HideInInspector] public NPC_Sheepling npc;
    [HideInInspector] public WanderState_Sheep wanderState;
    [HideInInspector] public FollowState_Sheep followState;
    [HideInInspector] public AvoidState_Sheep avoidState;
    
    // Following variables
    public GameObject Following;
    public float FollowDistance = 5.0f;
    
    private void OnEnable()
    {
        npc = this.GetComponent<NPC_Sheepling>();
        wanderState = this.GetComponent<WanderState_Sheep>();
        followState = this;
        avoidState = this.GetComponent<AvoidState_Sheep>();
    }

    public override void ExecuteStateAction()
    {
        if (!npc.chillin) //Slighty delay the following for more organic aesthetics
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