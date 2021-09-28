using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;


//this questitem is specifically working for the Shepperd quest
//however could be made more general by using inheritance or making the questgiver object public
public class QuestItem : InteractableItemBaseClass
{
    public int ItemID;

    public override void OnInteract()
    {

        //check if player in Pickup mode
        PlayerControllerAdapted.Mode mode = GameObject.Find("Player").GetComponent<PlayerControllerAdapted>().getMode();
        if (mode == PlayerControllerAdapted.Mode.Hand)
        {
            //check if this ques 
            int GatherID = GameObject.Find("Shepperd").GetComponent<QuestGiver>().quest.goal.GatherId;
            if (ItemID == GatherID)
            {
                GameObject.Find("Shepperd").GetComponent<QuestGiver>().quest.goal.ItemCollected();
                if (gameObject != null)
                {
                    //Destroy(gameObject);
                    MeshRenderer spawnItem = gameObject.GetComponent<MeshRenderer>();
                    spawnItem.enabled = false;
                }
            }
        }
    }
}