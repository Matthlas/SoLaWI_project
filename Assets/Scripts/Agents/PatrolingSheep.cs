using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolingSheep : MonoBehaviour
{
    private NavMeshAgent mAgent;

    private Animator mAnimator;

    // public GameObject Player;
    //
    // public float EnemyDistanceRun = 4.0f;
    
    //Patrol variables
    public Transform[] points;
    private int destPoint = 0;
    private bool chillin = false;

    // Use this for initialization
    void Start()
    {
        mAgent = GetComponent<NavMeshAgent>();
        
        mAnimator = GetComponent<Animator>();
    }

    IEnumerator ChillForABit()
    {
        Debug.Log("Chill for a bit");
        chillin = true;
        yield return new WaitForSeconds(20);
        GotoNextPoint();
        chillin = false;
    }


    void GotoNextPoint() {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        mAgent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }
    private bool IsNavMeshMoving
    {
        get
        { 
            return mAgent.velocity.magnitude > 0.1f;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!mAgent.pathPending && mAgent.remainingDistance < 1f && !chillin)
        {
            Debug.Log("In the if");
            StartCoroutine(ChillForABit());
        }
        mAnimator.SetBool("walk", IsNavMeshMoving);
    }
}
