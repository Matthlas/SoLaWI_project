using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



[System.Serializable] 
public class Quest
{
    // variables of Quest
    public bool isActive;
    public string title;

    public string description;
    
    //changethis
    public int experienceReward;
    public int goldReward;

    //make this as part of a quest directly?
    //public QuestGoal goal;

    public void Complete()
    {
        isActive = false;
        
    }

}
