using System.Collections.Generic;
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
    // public List<GameObject> objectsToAvoid;
    public float AvoidingDistance = 3.0f;
    private float[] distances;
    
    private void OnEnable()
    {
        npc = this.GetComponent<NPC_BaseClass>();
        idleState = this.GetComponent<IdleBasicState>();
        avoidState = this;
        // distances = new float[objectsToAvoid.Count];
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
        // float[] distances = new float[objectsToAvoid.Count];
        // foreach (var object in objectsToAvoid)
        // {
        //     
        // }
        if (Avoiding == null)
            return false;
        return Vector3.Distance(transform.position, Avoiding.transform.position) < AvoidingDistance;
    }
}