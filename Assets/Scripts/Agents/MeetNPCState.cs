using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(IdleBasicState))]
[RequireComponent(typeof(AvoidState))]

public class MeetNPCState : NPCState
{
    [HideInInspector] public NPC_Citizen1 npc;
    [HideInInspector] private IdleBasicState idleState;
    [HideInInspector] private AvoidState avoidState;
    
    private List<MeetNPCState> other_meetable_npcs_list = new List<MeetNPCState>();

    //Working variables
    [SerializeField] private float meetingDistance = 1f;
    [SerializeField] private float meetingTime = 5f;
    public MeetNPCState currentPartner = null;
    public bool inMeeting = false;
    private bool recentlyMetSomeone = false;
    
    private float rotation_speed = 5f;

    private void OnEnable()
    {
        npc = this.GetComponent<NPC_Citizen1>();
        idleState = this.GetComponent<IdleBasicState>();
        avoidState = this.GetComponent<AvoidState>();
    }
    

    public override void ExecuteStateAction()
    {
        if (currentPartner != null && !recentlyMetSomeone) 
        {
            if (PartnerFarAway())
            {
                Vector3 dirToFollowing = this.transform.position - currentPartner.transform.position;
                Vector3 newPos = npc.transform.position - dirToFollowing;
                //Calculate direction vector to partner and set new destination
                if (npc.navAgent.destination != newPos)
                    npc.navAgent.SetDestination(newPos);
            }
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
        if (avoidState.CloseToAvoiding())
            return avoidState;
        if (currentPartner != null && !recentlyMetSomeone)
            return this;
        else
            return idleState;
    }
    
    
    //if colliding chef itf the other is a meetable npc
    private void OnTriggerEnter(Collider other)
    {
        TryMeeting(other);
    }

    private void OnTriggerExit(Collider other)
    {
        MeetNPCState other_meetable_npc = other.GetComponent<MeetNPCState>();
        if (other_meetable_npc != null)
        {
            other_meetable_npcs_list.Remove(other_meetable_npc);
        }
    }

    private void TryMeeting(Collider other)
    {
        MeetNPCState other_meetable_npc = other.GetComponent<MeetNPCState>();
        
        if (other_meetable_npc != null)
        {
            if (other_meetable_npc.CanMeet())
                other_meetable_npcs_list.Add(other_meetable_npc);

            if (currentPartner == null && !recentlyMetSomeone)
                currentPartner = other_meetable_npc;
        }
    }

    public bool CanMeet()
    {
        return currentPartner == null && !recentlyMetSomeone;
    }

    public bool PartnerFarAway()
    {
        return Vector3.Distance(transform.position, currentPartner.transform.position) > meetingDistance;
    }

    private void StartMeeting()
    {
        inMeeting = true;
        npc.navAgent.isStopped = true;
        npc.navAgent.ResetPath();
        npc.navAgent.isStopped = false;
        Invoke("StopMeeting", meetingTime);
    }

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