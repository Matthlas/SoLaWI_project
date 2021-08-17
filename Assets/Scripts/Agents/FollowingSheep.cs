using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowingSheep : MonoBehaviour
{
    private NavMeshAgent mAgent;

    private Animator mAnimator;
    
    //Avoiding
    public GameObject Avoiding;
    public float AvoidingDistanceRun = 3.0f;
    public bool fleeing = false;
    
    // Following
    public GameObject Following;
    public float FollowDistance = 5.0f;

    //Patrol variables
    [SerializeField] private float ExplorationRadius = 2.0f;
    private bool chillin = false;

    
    void Start()
    {
        mAgent = GetComponent<NavMeshAgent>();
        
        mAnimator = GetComponent<Animator>();
    }

    IEnumerator ChillForABit()
    {
        chillin = true;
        mAgent.isStopped = true;
        yield return new WaitForSeconds(Random.Range(2.0f,10.0f));
        mAgent.isStopped = false;
        RandomExploration();
        chillin = false;
    }

    IEnumerator FollowRandomDelay()
    {
        yield return new WaitForSeconds(Random.Range(0.0f,4.0f));
        // Vector player to me
        Vector3 dirToFollowing = transform.position - Following.transform.position;
        Vector3 newPos = transform.position - (dirToFollowing/2);
        mAgent.SetDestination(newPos);
    }
    
    void RandomExploration() {
        // Create Random exploration destination
        Vector3 explorePosition = new Vector3(Random.Range(-ExplorationRadius, ExplorationRadius), Random.Range(-ExplorationRadius, ExplorationRadius), 1);
        Vector3 destination = this.transform.position + explorePosition;
        // Set the agent to go to the selected destination.
        mAgent.SetDestination(destination);
    }
    private bool IsNavMeshMoving
    {
        get
        { 
            return mAgent.velocity.magnitude > 0.1f;
        }
    }

    //TODO IMPLEMENT PROPERLY --> Better choose a finite state machine
    void Flee()
    {
        float squaredDist = (transform.position - Avoiding.transform.position).sqrMagnitude;
        float AvoidingDistanceRunSqrt = AvoidingDistanceRun * AvoidingDistanceRun;

        // Run away from player
        if (squaredDist < AvoidingDistanceRunSqrt)
        {
            // Vector player to me
            Vector3 dirToAvoiding = transform.position - Avoiding.transform.position;

            Vector3 newPos = transform.position + dirToAvoiding;

            mAgent.SetDestination(newPos);

        }
    }

    // Update is called once per frame
    void Update()
    {


        //Flee();
        // Chill();
        //Follow();
        if (!mAgent.pathPending && mAgent.remainingDistance < 1f && !chillin)
        {
            StartCoroutine(ChillForABit());
        }
        float squaredDist = (transform.position - Following.transform.position).sqrMagnitude;
        float FollowingDistanceRunSqrt = FollowDistance * FollowDistance;

        // Run halfway towards Following if too far away
        if (squaredDist > FollowingDistanceRunSqrt)
        {
            StartCoroutine(FollowRandomDelay());

        }
        else
        {
            if (!chillin)
            {
                StartCoroutine(ChillForABit());
            }
        }
        
        
        mAnimator.SetBool("walk", IsNavMeshMoving);
    }
}