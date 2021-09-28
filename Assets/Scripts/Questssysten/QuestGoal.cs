using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;
    public int requiredAmount;
    public int currentAmount;
    //public GameObject GatherItem;
    public int GatherId;

    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }

    //call this function from Challengemanger when item is collected
    public void ItemCollected()
    {
        if (goalType == GoalType.Gathering)
        {
            currentAmount++;
        }

    }

    public enum GoalType
    {
        Gathering,
        escort,
        performaction
    }
}    
     