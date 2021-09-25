using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NPC_BaseClass))]
[RequireComponent(typeof(IdleBasicState))]

public class AvoidState : NPCState
{
    [HideInInspector] public NPC_BaseClass npc;
    [HideInInspector] private IdleBasicState idleState;

    //Avoiding variables
    public List<GameObject> objectsToAvoid;
    public float AvoidingDistance = 3.0f;
    private GameObject Avoiding;
    
    private void OnEnable()
    {
        // Instantiate all variables
        npc = this.GetComponent<NPC_BaseClass>();
        idleState = this.GetComponent<IdleBasicState>();
        Avoiding = null;
    }
    

    public override void ExecuteStateAction()
    {
        if (Avoiding != null)
        {
            //Calculate direction vector to avoiding and set new destination
            Vector3 dirToAvoiding = npc.transform.position - Avoiding.transform.position;
            Vector3 newPos = npc.transform.position + dirToAvoiding;
        
            if (npc.navAgent.destination != newPos)
                npc.navAgent.SetDestination(newPos);
        }


    }

    public override NPCState TransitionToState()
    {
        //Define transition conditions
        if (CloseToAvoiding())
            return this;
        else
            return idleState;

    }
    
    public bool CloseToAvoiding()
    {
        //Calculate distances to all objects to avoid
        //Make the closest object the one to Avoid
        float closest_dist = AvoidingDistance + 1;
        Avoiding = null;
        foreach (var obj in objectsToAvoid)
        {
            float distToObj = Vector3.Distance(transform.position, obj.transform.position);
            if (distToObj < closest_dist)
            {
                closest_dist = distToObj;
                Avoiding = obj;
            }
                
        }
        //Return true if there is an object to avoid, false otherwise
        if (Avoiding == null)
            return false;
        return closest_dist < AvoidingDistance;
    }
}