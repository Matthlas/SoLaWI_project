using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PerformTaskState))]
[RequireComponent(typeof(NPC_Task_Controller))]
public class IdleState_Citizen : IdleBasicState
{
    public PerformTaskState performTaskState;
    private NPC_Task_Controller _taskController;

    protected override void OnEnable()
    {
        npc = this.GetComponent<NPC_BaseClass>();
        idleState = this;
        avoidState = this.GetComponent<AvoidState>();
        performTaskState = this.GetComponent<PerformTaskState>();
        _taskController = this.GetComponent<NPC_Task_Controller>();
    }
    
    public override void ExecuteStateAction()
    {

    }

    public override NPCState TransitionToState()
    {
        if (avoidState.CloseToAvoiding())
            return avoidState;
        else if (!_taskController.activeTask.done )
            return performTaskState;
        else
            return this;
    }

}
