using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PerformTaskState))]
[RequireComponent(typeof(NPC_Task_Controller))]
public class IdleState_Citizen : IdleBasicState
{
    public PerformTaskState performTaskState;
    private NPC_Task_Controller _taskController;
    private MeetNPCState meetState;
    [HideInInspector] private MeetPlayerState meetPlayerState;

    protected override void OnEnable()
    {
        npc = this.GetComponent<NPC_BaseClass>();
        idleState = this;
        avoidState = this.GetComponent<AvoidState>();
        performTaskState = this.GetComponent<PerformTaskState>();
        _taskController = this.GetComponent<NPC_Task_Controller>();
        meetState = this.GetComponent<MeetNPCState>();
        meetPlayerState = this.GetComponent<MeetPlayerState>();
    }
    
    public override void ExecuteStateAction()
    {

    }

    public override NPCState TransitionToState()
    {
        if (avoidState.CloseToAvoiding())
            return avoidState;
        if (meetPlayerState.meetingPlayer)
            return meetPlayerState;
        
        if (meetState != null)
        {
            if (meetState.currentPartner != null)
            {
                return meetState;
            }
        }

        if (_taskController.activeTask != null)
        {
            if (!_taskController.activeTask.done)
                return performTaskState;
        }
        
        return idleState;
    }
}
