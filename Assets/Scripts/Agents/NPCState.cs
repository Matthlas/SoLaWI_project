using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Basic Interface that each state must implement
public abstract class NPCState : MonoBehaviour
{
    public virtual NPCState DoState()
    {
        ExecuteStateAction();
        return TransitionToState();
    }

    public abstract void ExecuteStateAction();

    public abstract NPCState TransitionToState();
}
