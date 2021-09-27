using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;



[System.Serializable] 
public class Quest: MonoBehaviour
{
    private void Start()
    {
        Debug.Log("start");
    }

    // variables of Quest
    public bool isActive;
    public string title;

    public string description;
    
    //changethis
    public int experienceReward;
    public int goldReward;

    // variables of questwindow
    public GameObject questWindow;
    public Text titleText;
    public Text descriptionText;
    
    //change
    public Text experienceText;
    public Text goldText;
    
    //make this as part of a quest directly?
    //public QuestGoal goal;

    public void Complete()
    {
        isActive = false;
        
    }
    
    
    //add this function to UI button
    public void OpenQuestWindow()
    {
        //accesses the variables from the quest and displays them in the UI
        questWindow.SetActive(true);
        titleText.text = title;
        descriptionText.text = description;
        
        //change
        experienceText.text = experienceReward.ToString();
        goldText.text = goldReward.ToString();
    }

    //add this function to UI button
    public void AcceptQuest()
    {
        questWindow.SetActive(false);
        isActive = true;
        //ToDo: give Quest to questmanager
        

    }

}