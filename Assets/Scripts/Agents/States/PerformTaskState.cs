using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(IdleBasicState))]
[RequireComponent(typeof(AvoidState))]
[RequireComponent(typeof(NPC_Task_Controller))]

public class PerformTaskState : NPCState
{
    [HideInInspector] public NPC_Citizen1 npc;
    [HideInInspector] private IdleBasicState idleState;
    [HideInInspector] private AvoidState avoidState;
    [HideInInspector] private NPC_Task_Controller _taskController;
    [HideInInspector] private MeetPlayerState meetPlayerState;
    private MeetNPCState meetState;
    private NPCTask myTask;
    
    //Working variables
    private float WorkingDistance = 2f;

    private void OnEnable()
    {
        npc = this.GetComponent<NPC_Citizen1>();
        idleState = this.GetComponent<IdleBasicState>();
        avoidState = this.GetComponent<AvoidState>();
        _taskController = this.GetComponent<NPC_Task_Controller>();
        meetState = this.GetComponent<MeetNPCState>();
        meetPlayerState = this.GetComponent<MeetPlayerState>();
    }
    

    public override void ExecuteStateAction()
    {
        myTask = _taskController.currentTask();
        if (FarFromTask())
        {
            //Calculate direction vector to following and set new destination
            Vector3 dirToTask = npc.transform.position - myTask.location;
            Vector3 newPos = npc.transform.position - dirToTask;

            if (npc.navAgent.destination != newPos)
                npc.navAgent.SetDestination(newPos);
        }
        else
        {
            if (!myTask.active && !myTask.done)
            {
                myTask.StartTask();
                npc.animateWorking();
            }
            else if (myTask.active)
            {
                npc.animateWorking();
            }
        }
    }

    public override NPCState TransitionToState()
    {
        if (avoidState.CloseToAvoiding())
            return avoidState;
        if (meetPlayerState.meetingPlayer)
            return meetPlayerState;
        if (meetState != null)
            if (meetState.currentPartner != null)
                return meetState;
        if (myTask.done)
        {
            return idleState;
        }
        else
            return this;
    }

    private bool FarFromTask()
    {
        return Vector3.Distance(transform.position, myTask.location) > WorkingDistance;
    }

}