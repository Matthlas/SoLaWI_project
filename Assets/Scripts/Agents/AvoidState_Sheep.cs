using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(WanderState_Sheep))]
[RequireComponent(typeof(FollowState_Sheep))]
public class AvoidState_Sheep : NPCState
{
    [HideInInspector] public NPC_Sheepling npc;
    [HideInInspector] private WanderState_Sheep wanderState;
    [HideInInspector] private FollowState_Sheep followState;
    [HideInInspector] private AvoidState_Sheep avoidState;
    
    //Avoiding variables
    public GameObject Avoiding;
    public float AvoidingDistance = 3.0f;
    
    private void OnEnable()
    {
        npc = this.GetComponent<NPC_Sheepling>();
        wanderState = this.GetComponent<WanderState_Sheep>();
        followState = this.GetComponent<FollowState_Sheep>();
        avoidState = this;
    }
    

    public override void ExecuteStateAction()
    {
        //Calculate direction vector to avoiding and set new destination
        Vector3 dirToAvoiding = npc.transform.position - avoidState.Avoiding.transform.position;
        Vector3 newPos = npc.transform.position + dirToAvoiding;
        
        if (npc.navAgent.destination != newPos)
            npc.navAgent.SetDestination(newPos);

    }

    public override NPCState TransitionToState()
    {
        if (CloseToAvoiding())
            return avoidState;
        else if (!followState.FarFromFollowing())
            //Possibly stop agent
            return wanderState;
        else
            return followState;
    }
    
    public bool CloseToAvoiding()
    {
        return Vector3.Distance(transform.position, Avoiding.transform.position) < AvoidingDistance;
    }
}