using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class NPC_BaseClass : MonoBehaviour
{
    [SerializeField]
    private string currentStateName; //Displays current state in Game Manager
    public NPCState currentState;

    public NavMeshAgent navAgent;
    public Vector3 nextLocation;
    
    //Animation
    protected Animator mAnimator;
    
    //Chill variables
    public float chillTime = 5.0f;
    public bool chillin = false;
    
    //Initialize all possible States here:
    

    
    public virtual void OnEnable()
    {
        currentState = null;
        Debug.Log("Why am I here?");
        if (mAnimator == null)
            mAnimator = GetComponent<Animator>();
        if (navAgent == null)
            navAgent = GetComponent<NavMeshAgent>();
    }
    
    private bool IsNavMeshMoving
    { 
        get
        { 
            return navAgent.velocity.magnitude > 0.1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Each frame the current state is executed and if a state transition is triggered
        //in the execution of the state the state is changed
        currentState = currentState.DoState();
        currentStateName = currentState.ToString();
        Debug.Log("StateName in BaseClass: " + currentStateName);

        animate();
    }

    public void animate()
    // Override this method for different agents with different walking animations
    {
        //Walk animation if the agent is moving
        mAnimator.SetBool("walk", IsNavMeshMoving);
    }

}
