using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePatrolPoints : MonoBehaviour
{
    public Transform[] points;
    private int destPoint = 0;
    [SerializeField] public float WaitSeconds = 20.0f;
    private bool chillin = false;

    IEnumerator WaitAndMove()
    {
        chillin = true;
        yield return new WaitForSeconds(WaitSeconds);
        GotoNextPoint();
        chillin = false;
    }
    
    void GotoNextPoint() {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        transform.position = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (!chillin)
        {
            StartCoroutine(WaitAndMove());
        }
    }
}
