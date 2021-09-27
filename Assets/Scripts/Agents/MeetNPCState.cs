using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(IdleBasicState))]

public class MeetNPCState : NPCState
{
    [HideInInspector] private NPC_Citizen1 npc;
    [HideInInspector] private IdleBasicState idleState;
    [HideInInspector] private AvoidState avoidState;
    

    //Meeting variables
    [SerializeField] private float meetingDistance = 1f;
    [SerializeField] private float meetingTime = 5f;
    public MeetNPCState currentPartner = null;
    public bool inMeeting = false;
    private bool recentlyMetSomeone = true;
    
    private float rotation_speed = 5f;

    private void OnEnable()
    {
        npc = this.GetComponent<NPC_Citizen1>();
        idleState = this.GetComponent<IdleBasicState>();
        avoidState = this.GetComponent<AvoidState>();
        // Slightly delay meeting so NPCs don't start out directly meeting
        Invoke("ReadyToMeetAgain", meetingTime);
    }
    

    public override void ExecuteStateAction()
    {
        if (currentPartner != null && !recentlyMetSomeone) 
        {
            // Walk towards partner if far away
            if (PartnerFarAway())
            {
                Vector3 dirToFollowing = this.transform.position - currentPartner.transform.position;
                Vector3 newPos = npc.transform.position - dirToFollowing;
                //Calculate direction vector to partner and set new destination
                if (npc.navAgent.destination != newPos)
                    npc.navAgent.SetDestination(newPos);
            }
            //If close initiate meeting, play talking animation and rotate towards the partner
            else
            {
                if (!inMeeting)
                    StartMeeting();
                else
                    npc.animateTalking();
                RotateTowards(currentPartner.transform);
            }
            
        }
        
            
    }

    public override NPCState TransitionToState()
    {
        if (avoidState != null)
        {
            if (avoidState.CloseToAvoiding())
                return avoidState;
        }
        if (currentPartner != null && !recentlyMetSomeone)
            return this;
        else
            return idleState;
    }
    
    
    //if colliding check it the other is a meetable npc
    private void OnTriggerEnter(Collider other)
    {
        TryMeeting(other);
    }

    //On trigger exit remove the other as partner. THis shouldn't happen but better safe than sorry
    private void OnTriggerExit(Collider other)
    {
        MeetNPCState other_meetable_npc = other.GetComponent<MeetNPCState>();
        if (other_meetable_npc != null)
        {
            if (other_meetable_npc == currentPartner)
                currentPartner = null;
        }
    }

    // If the other can meet, and this can meet set other as current partner
    private void TryMeeting(Collider other)
    {
        MeetNPCState other_meetable_npc = other.GetComponent<MeetNPCState>();
        
        if (other_meetable_npc != null)
        {
            if (other_meetable_npc.CanMeet() && this.CanMeet())
            {
                currentPartner = other_meetable_npc;
                other_meetable_npc.currentPartner = this;
            }
        }
    }

    // Conditions for being able to meet
    public bool CanMeet()
    {
        return currentPartner == null && !recentlyMetSomeone && !inMeeting;
    }

    public bool PartnerFarAway()
    {
        return Vector3.Distance(transform.position, currentPartner.transform.position) > meetingDistance;
    }

    // Initiate meeting, stop agent
    private void StartMeeting()
    {
        inMeeting = true;
        npc.navAgent.isStopped = true;
        npc.navAgent.ResetPath();
        npc.navAgent.isStopped = false;
        Invoke("StopMeeting", meetingTime);
    }

    // stop meeting and initiate delay until next meeting can happen
    private void StopMeeting()
    {
        inMeeting = false;
        recentlyMetSomeone = true;
        currentPartner = null;
        Invoke("ReadyToMeetAgain", meetingTime * 2);
        CancelInvoke("StopMeeting");
    }

    private void ReadyToMeetAgain()
    {
        recentlyMetSomeone = false;
    }
    
    private void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotation_speed);
        }

    }



}