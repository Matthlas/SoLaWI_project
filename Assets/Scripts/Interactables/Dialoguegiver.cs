using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;



public class Dialoguegiver : InteractableItemBaseClass
{
    //maybe make array of dialogues?
    public Dialogue dialogue1;
    public Dialogue dialogue2;
    public Dialogue dialogue3;

    [SerializeField]
    public GameObject QuestItem;

    public Collider n_collider;
    
  
    public static bool Qcompleted = false;
    //finite state machine for Quest/NPC status
    public enum NPCFSM {
        inactive,
        pending,
        completed,
    }
    // default state
    public NPCFSM NPCMode = NPCFSM.inactive;
    

    public void triggerDialogue(Dialogue dialogue)
    { 
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
    
    // depending on the state of the NPC's quest a different dialogue is played

    public override void OnInteract()
    {
       // n_collider = this.gameObject.GetComponent. < CapsuleCollider > ();
        if (Qcompleted == true)
        {
            
            NPCMode = NPCFSM.completed;
        }
        if (NPCMode == NPCFSM.completed)
        {
            triggerDialogue(dialogue3);
            //n_collider.enabled  = true;
            //insert something like: if pressed continue then change npc mod
        }
        if (NPCMode == NPCFSM.pending)
        {
            triggerDialogue(dialogue2);
            //insert something like: if pressed continue then change npc mode
            //spawns quest Item
            MeshRenderer spawnItem = QuestItem.GetComponent<MeshRenderer>();
            spawnItem.enabled = true;

        }
        if (NPCMode == NPCFSM.inactive)
        {
            triggerDialogue(dialogue1);
            //insert something like: if pressed continue then change npc mode
            NPCMode = NPCFSM.pending;
        }
        

    }

    public object CapsuleCollider { get; set; }
}




