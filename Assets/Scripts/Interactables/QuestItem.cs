using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;



public class QuestItem : InteractableItemBaseClass
{

    public override void OnInteract()
    {
        //destroy game object
        //Destroy(gameObject);
        MeshRenderer spawnItem = gameObject.GetComponent<MeshRenderer>();
        spawnItem.enabled = false;
        //play cue
        //set quest to completed /alternative: directly change mode of npc
        Dialoguegiver.Qcompleted = true;


    }







}