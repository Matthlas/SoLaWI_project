using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreboardHandler : MonoBehaviour
{
    private static int CabbageScore = 0;
    private static int CabbageMaxScore = 0;
    private static int TomatoScore = 0;
    private static int TomatoMaxScore = 0;
    private static int BeetScore = 0;
    private static int BeetMaxScore = 0;
    private static int unknownPlantScore = 0;
    
    private static TextMeshProUGUI text;

    private void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    public static void newScore(SeedListener.PlantSeeds plant, int addScore)
    {
        switch (plant)
        {
            case SeedListener.PlantSeeds.Beet:
                BeetScore += addScore;
                break;
            case SeedListener.PlantSeeds.Cabbage:
                 CabbageScore+= addScore;
                break;
            case SeedListener.PlantSeeds.Tomato:
                TomatoScore += addScore;
                break;
            default:
                unknownPlantScore += addScore;
                break;
        }

        displayScore();
        //wenn nicht alle max auf 0 sind und alle scores erreicht sind
        if (!(BeetMaxScore==0 & TomatoMaxScore ==0 & CabbageMaxScore == 0) &
            (BeetScore >= BeetMaxScore & TomatoScore >= TomatoMaxScore & CabbageScore >= CabbageMaxScore))
        {
            ChallengeManager.challengeAccomplished();
        }
    }

    public static void displayScore()
    {
        string newText = "";
        if (BeetScore > 0 | BeetMaxScore > 0)
        {
            if (BeetMaxScore > 0)
            {
                newText = newText.Insert(newText.Length, "Beets: " + BeetScore +
                                                         "/" + BeetMaxScore + "\n");
            }
            else
            {
                newText = newText.Insert(newText.Length, "Beets: " + BeetScore + "\n");
            }
            
        }
        if (TomatoScore > 0 | TomatoMaxScore > 0)
        {
            if (TomatoMaxScore > 0)
            {
                newText = newText.Insert(newText.Length, "Tomatoes: " + TomatoScore +
                                                         "/" + TomatoMaxScore + "\n");
            }
            else
            {
                newText = newText.Insert(newText.Length, "Tomatoes: " + TomatoScore + "\n");
            }
        }
        if (CabbageScore > 0 | CabbageMaxScore > 0)
        {
            if (CabbageMaxScore > 0)
            {
                newText = newText.Insert(newText.Length, "Cabbages: " + CabbageScore +
                                                         "/" + CabbageMaxScore + "\n");
            }
            else
            {
                newText = newText.Insert(newText.Length, "Cabbages: " + CabbageScore + "\n");
            }
        }
        if (unknownPlantScore > 0)
        {
            newText = newText.Insert(newText.Length, "Unknown Plants: " + unknownPlantScore + "\n");
        }

        text.text = newText;
    }

    public static void newChallenge(int[] newValues)
    {
        BeetScore = BeetScore - BeetMaxScore;
        BeetMaxScore = newValues[0];
        TomatoScore = TomatoScore - TomatoMaxScore;
        TomatoMaxScore = newValues[1];
        CabbageScore = CabbageScore - CabbageMaxScore;
        CabbageMaxScore = newValues[2];
    }
}
