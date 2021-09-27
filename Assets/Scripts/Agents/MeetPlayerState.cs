using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(IdleBasicState))]

public class MeetPlayerState : NPCState
{
    [HideInInspector] private NPC_Citizen1 npc;
    [HideInInspector] private IdleBasicState idleState;
    [HideInInspector] private AvoidState avoidState;
    

    //Meeting variables
    [SerializeField] private float meetingPlayerTime = 10f;
    private GameObject player = null;
    public bool meetingPlayer = false;

    private float rotation_speed = 5f;

    private void OnEnable()
    {
        npc = this.GetComponent<NPC_Citizen1>();
        idleState = this.GetComponent<IdleBasicState>();
        avoidState = this.GetComponent<AvoidState>();
        // Slightly delay meeting so NPCs don't start out directly meeting
    }
    

    public override void ExecuteStateAction()
    {
        if (meetingPlayer) 
        {
            RotateTowards(player.transform);
        }
    }

    public override NPCState TransitionToState()
    {
        if (avoidState != null)
        {
            if (avoidState.CloseToAvoiding())
                npc.stopAllTalking();
                return avoidState;
        }
        if (meetingPlayer)
            return this;
        else
            return idleState;
    }
    
    
    //if colliding check it the other is player and meet
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            MeetPlayer(other);
    }

    //On trigger exit stop meeting player
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            StopMeetingPlayer();
    }

    
    // If the other can meet, and this can meet set other as current partner
    public void MeetPlayer(Collider other)
    {
        meetingPlayer = true;
        player = other.gameObject;
        npc.navAgent.isStopped = true;
        npc.navAgent.ResetPath();
        npc.navAgent.isStopped = false;
        npc.animateWaving();
        Invoke("StopMeetingPlayer", meetingPlayerTime);
    }
    
    private void StopMeetingPlayer()
    {
        meetingPlayer = false;
        player = null;
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