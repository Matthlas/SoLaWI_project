using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPC : InteractableItemBaseClass
{

    public Dialogue dialogue;
 
    /*finite state machine for Quest/NPC status
    public enum NPCFSM
    {
        inactive,
        plain,
        planted
    }
    // default state
    public BedFSM bedMode = BedFSM.plain;
    if (bedMode != BedFSM.inactive)
        {

        }
    */

    public void triggerDialogue()
    { 
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public override void OnInteract()
    {
        triggerDialogue();
    }







}

