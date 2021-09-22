using UnityEngine;
using UnityEngine.AI;

public class DummyState : NPCState
{
    public override void ExecuteStateAction()
    {
        Debug.Log("DummyState: Does nothing, always transitions to itself");
    }

    public override NPCState TransitionToState()
    {
        return this;
    }
    
}