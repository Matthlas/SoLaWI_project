using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    //public Questmanager questmanager;


    public GameObject questWindow;
    public Text titleText;
    public Text descriptionText;
    
    //change
    public Text experienceText;
    public Text goldText;

    //add this function to UI button
    public void OpenQuestWindow()
    {
        //accesses the variables from the quest and displays them in the UI
        questWindow.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        
        //change
        experienceText.text = quest.experienceReward.ToString();
        goldText.text = quest.goldReward.ToString();
    }

    //add this function to UI button
    public void AcceptQuest()
    {
        questWindow.SetActive(false);
        quest.isActive = true;
        //needs to be changed bacause we don't want the quest to be given to player
       // player.quest = quest;

    }
}
