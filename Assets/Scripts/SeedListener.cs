using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedListener : MonoBehaviour
{
    [SerializeField]
    public KeyCode _key;
    
    public enum PlantSeeds
    {
        Cabbage,
        Tomato,
        Beet
    }

    private PlantSeeds[] plants = new PlantSeeds[3];

    private int plantMode = 0;
    private static PlantSeeds currentPlant;

    private void Start()
    {
        plants = new PlantSeeds[3];
        plants[0] = PlantSeeds.Beet;
        plants[1] = PlantSeeds.Tomato;
        plants[2] = PlantSeeds.Cabbage;
        currentPlant = plants[0];
    }

    void Update()
    {
        if(Input.GetKeyDown(_key))
        {
            if (plantMode == plants.Length-1)
            {
                plantMode = 0;

            }
            else
            {
                ++plantMode;
            }

            currentPlant = plants[plantMode];

            //TODO: Change Picture of Seed in inventory depending on current plant

        }
    }

    public static PlantSeeds getCurrentPlant()
    {
        return currentPlant;
    }
    
}
