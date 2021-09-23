using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(IdleBasicState))]
[RequireComponent(typeof(AvoidState))]
public class WorkingState_Citizen : NPCState
{
    [HideInInspector] public NPC_Citizen1 npc;
    [HideInInspector] private IdleBasicState idleState;
    [HideInInspector] private AvoidState avoidState;
    
    //Avoiding variables
    public float workingTime = 5.0f;
    private bool workingActive = false;
    private bool workHereDone = false;
    
    private void OnEnable()
    {
        npc = this.GetComponent<NPC_Citizen1>();
        idleState = this.GetComponent<IdleBasicState>();
        avoidState = this.GetComponent<AvoidState>();
    }
    

    public override void ExecuteStateAction()
    {
        if (!workingActive)
        {
            workingActive = true;
            Invoke("workDone", workingTime);
        }
        npc.animateWorking();
        
    }

    public override NPCState TransitionToState()
    {
        if (avoidState.CloseToAvoiding())
            return avoidState;
        if (workHereDone)
        {
            workHereDone = false;
            return idleState;
        }
        else
            return this;
    }

    private void workDone()
    {
        workingActive = false;
        workHereDone = true;
    }
    
}