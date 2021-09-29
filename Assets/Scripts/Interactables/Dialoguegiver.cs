using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;



public class Dialoguegiver : InteractableItemBaseClass
{
    //maybe make array of dialogues?
    public Dialogue dialogue1;
    public Dialogue dialogue2;
    public Dialogue dialogue3;
    public Dialogue dialogue4;
    
    public GameObject q_trigger;
    [SerializeField]

    //finite state machine for Quest/NPC status
    public enum NPCFSM {
        inactive,
        pending,
        completed,
        afterquest
    }
    // default state
    public NPCFSM NPCMode = NPCFSM.inactive;

    public void triggerDialogue(Dialogue dialogue)
    {
        if (FindObjectOfType<DialogueManager>().inDialogue == false)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
        else
        {
            return;
        }
        
    }
    
    // depending on the state of the NPC's quest a different dialogue is played

    public override void OnInteract()
    {
        
        // the dialoggiver mode is set to completed, when the quest is completed
        bool Qcompleted = GameObject.Find("Hiker").GetComponent<QuestGiver>().quest.completed;
        if (Qcompleted == true)
        {
            NPCMode = NPCFSM.completed;
        }
        if (NPCMode == NPCFSM.afterquest)
        {
            triggerDialogue(dialogue4);
            //n_collider.enabled  = true;
            //insert something like: if pressed continue then change npc mod
        }
        if (NPCMode == NPCFSM.completed)
        {   
            q_trigger.SetActive(false);
            triggerDialogue(dialogue3);
            NPCMode = NPCFSM.afterquest;


                //n_collider.enabled  = true;
        }
        if (NPCMode == NPCFSM.pending)
        {
            triggerDialogue(dialogue2);
            
            //if the NPC has a quest then the third dialogue only gets activated after the quest is completed
            if (q_trigger != null)
            {
                //spawns a button that opens the questwindow
                q_trigger.SetActive(true);
            }
            else
            {
                NPCMode = NPCFSM.completed;
            }


            //spawns quest Items (must get debugged)
            //MeshRenderer spawnItem = QuestItem.GetComponent<MeshRenderer>();
            //spawnItem.enabled = true;

        }
        if (NPCMode == NPCFSM.inactive)
        {
            triggerDialogue(dialogue1);
            //insert something like: if pressed continue then change npc mode
            
            NPCMode = NPCFSM.pending;
            
            
           
            
        }
        

    }
    
}




