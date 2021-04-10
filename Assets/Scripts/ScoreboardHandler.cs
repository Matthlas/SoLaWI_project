using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreboardHandler : MonoBehaviour
{
    private static int CabbageScore = 0;
    private static int TomatoScore = 0;
    private static int BeetScore = 0;
    private static int unknownPlantScore = 0;
    
    private static TextMeshProUGUI text;

    private void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        displayScore();
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
    }

    static void displayScore()
    {
        string newText = "";
        if (BeetScore > 0)
        {
            newText = newText.Insert(newText.Length, "Beets: " + BeetScore + "\n");
        }
        if (TomatoScore > 0)
        {
            newText = newText.Insert(newText.Length, "Tomatoes: " + TomatoScore + "\n");
        }
        if (CabbageScore > 0)
        {
            newText = newText.Insert(newText.Length, "Cabbages: " + CabbageScore + "\n");
        }
        if (unknownPlantScore > 0)
        {
            newText = newText.Insert(newText.Length, "Unknown Plants: " + unknownPlantScore + "\n");
        }

        text.text = newText;
    }
}
