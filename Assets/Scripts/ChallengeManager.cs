using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class ChallengeManager : MonoBehaviour
{
    private static int challengeNr = 1;
    private static GameObject thisGameObject;
    public Quest quest;
        
    
    void Start()
    {
        newChallenge();
        thisGameObject = gameObject;
    }

    public static void newChallenge()
    {
        GameObject.Find("ChallengeManager").GetComponentInChildren<Image>().enabled = true;
        GameObject.Find("ChallengeManager").transform.GetChild(1).GetComponent<TextMeshProUGUI>().enabled = true;
        if (challengeNr == 1)
        {
            GameObject.Find("ChallengeManager").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                "Fed up with life and the way things are going, you decided to get out of " +
                "the capitalist system, you detest so much. You felt lonely and isolated " +
                "and remembered a place from your childhood, a place where your mommy used " +
                "to take you: \n <b> SoLaWille </b> \n To start your new life you want " +
                "to get back to nature. And what better way to reconnect with your roots than " +
                "to plant some root vegetables? Your favorite root vegetable is the beet. \n" +
                "<b>Harvest 10 Beets! </b>\n\n" +
                "Press K to close window";
            ScoreboardHandler.newChallenge(new int[]{10,0,0});
        }
        else if (challengeNr == 2)
        {
            GameObject.Find("ChallengeManager").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                "You did it! You feel better already. You gave your mommy a taste of your delicious" +
                " beets, because she is the only one you want to share with. Everyone else" +
                " was mean to you most of the time. But she told all her friends about your" +
                " delicious Beets! Now some of her friends are interested and want to try your beets." +
                "You don't really have anything better to do, so you go on with gardening." +
                " Additionally to the Beets, you'll try Tomatoes and Cabbages," +
                " because you love autumnal tomatosoups. \n" +
                "<b>Harvest 30 Beets, 10 Tomatoes and 10 Cabbages! Beware of the weeds. If they get too many they hurt your plants </b> \n\n" +
                "Press K to close window";   
            ScoreboardHandler.newChallenge(new int[]{30,10,10});
        }
        else if (challengeNr == 3)
        {
            GameObject.Find("ChallengeManager").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                "The people love your food! You even found new friends! People who love good " +
                "food can only be kind people. You now found your new destiny: You will become" +
                " a farmer for a community supported agriculture. You will find your spirituality" +
                " and your love for music. The guitar really helps you connect with your real true " +
                "authentic inner self. For your new agriculture model, you need to \n" +
                "<b>Harvest 50 Beets, 50 Tomatoes and 50 Cabbages!</b> \n" +
                "But don't work too much. Take a break and play some guitar from time to time. \n \n" +
                "Additional Challenge: You think there might be some more fertile soil around to enlarge your farm. Perhaps try digging up some new beds...\n\n" +
                "Press K to close window";
            ScoreboardHandler.newChallenge(new int[]{50,50,50});
        }
        else
        {
            GameObject.Find("ChallengeManager").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                "Wow. Just wow. You and your vegetables and friends can now live in Unity " +
                "happily ever after";
            ScoreboardHandler.newChallenge(new int[]{0,0,0});
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            this.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().enabled = false;
            this.gameObject.GetComponentInChildren<Image>().enabled = false;
            ScoreboardHandler.displayScore();
        }
        
        //chacks whether questgoals are reached
        if(quest.isActive)
        {
            if (quest.goal.IsReached())
            {
                quest.Complete();
            }
            
        }
    }

    public static void challengeAccomplished()
    {
        ParticleSystem[] interactionCues =
            GameObject.Find("BedBlock").GetComponentsInChildren<ParticleSystem>();
        foreach (var cue in interactionCues)
        {
           cue.Play(); 
        }
        challengeNr++;
        newChallenge();

    }

}
