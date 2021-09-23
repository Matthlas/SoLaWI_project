using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(IdleBasicState))]
public class AvoidState : NPCState
{
    [HideInInspector] public NPC_BaseClass npc;
    [HideInInspector] private IdleBasicState idleState;
    [HideInInspector] private AvoidState avoidState;
    
    //Avoiding variables
    public GameObject Avoiding;
    public float AvoidingDistance = 3.0f;
    
    private void OnEnable()
    {
        npc = this.GetComponent<NPC_BaseClass>();
        idleState = this.GetComponent<IdleBasicState>();
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
        else
            return idleState;

    }
    
    public bool CloseToAvoiding()
    {
        return Vector3.Distance(transform.position, Avoiding.transform.position) < AvoidingDistance;
    }
}