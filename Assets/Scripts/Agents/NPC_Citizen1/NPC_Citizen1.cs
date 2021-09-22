using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//NPC structure inspired and adapted from
//https://github.com/onewheelstudio/Programming-Patterns/tree/master/Programming%20Patterns/Assets/State%20Pattern/Scripts/FSM
//https://www.youtube.com/watch?v=nnrOhb5UdRc


[RequireComponent(typeof(NavMeshAgent))]
public class NPC_Citizen1 : MonoBehaviour
{
    [SerializeField]
    private string currentStateName; //Displays current state in Game Manager
    private INPCState_Citizen1 currentState;

    // States --> initialize all states this NPC needs
    public WanderState_Citizen1 wanderState = new WanderState_Citizen1();

    public NavMeshAgent navAgent;

    public Vector3 nextLocation;
    
    //Avoiding
    public GameObject Avoiding;
    public float AvoidingDistance = 3.0f;

    // Following
    public GameObject Following;
    public float FollowDistance = 5.0f;
    
    //Wandering
    public float ExplorationRadius = 2.0f;
    public float chillTime = 1.0f;
    public bool chillin = false;
    

    //Animation
    private Animator mAnimator;
    
    
    private void OnEnable()
    {
        ChillABit();
        currentState = wanderState;
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
        currentState = currentState.DoState(this);
        currentStateName = currentState.ToString();
        
        //Walk animation if the agent is moving
        mAnimator.SetBool("run", IsNavMeshMoving);
    }
    
    public bool FarFromFollowing()
    {
        return Vector3.Distance(transform.position, Following.transform.position) > FollowDistance;
    }
    
    public bool CloseToAvoiding()
    {
        return Vector3.Distance(transform.position, Avoiding.transform.position) < AvoidingDistance;
    }

    //Method used to delay the wandering by *ChillTime*. Has to be called in the NPC class
    //since Invoke is a method from "MonoBehaviour"
    public void ChillABit()
    {
        if (!chillin)
        {
            chillin = true;
            Invoke("StopChilling", chillTime + Random.Range(-chillTime/2, chillTime));
        }
    }
    
    public void StopChilling()
    {
        chillin = false;
        navAgent.isStopped = false;
    }
}