using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPC : InteractableItemBaseClass
{

    public Dialogue dialogue1;
    public Dialogue dialogue2;
    //finite state machine for Quest/NPC status
    public enum NPCFSM {
        inactive,
        pending,
        completed
    }
    // default state
    public NPCFSM NPCMode = NPCFSM.inactive;
    
    

    public void triggerDialogue(Dialogue dialogue)
    { 
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public override void OnInteract()
    {   
        if (NPCMode == NPCFSM.pending)
        {
            triggerDialogue(dialogue2);
            //insert something like: if pressed continue then change npc mode
            NPCMode = NPCFSM.pending;
        }
        if (NPCMode == NPCFSM.inactive)
        {
            triggerDialogue(dialogue1);
            //insert something like: if pressed continue then change npc mode
            NPCMode = NPCFSM.pending;
        }
        else
        {
            return;
        }

    }







}

