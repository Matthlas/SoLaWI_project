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
        Carrot,
        Cucumber,
        Tomato
    }

    private PlantSeeds[] plants;

    private int plantMode = 0;
    private static PlantSeeds currentPlant;

    private void Start()
    {
        plants = new PlantSeeds[3];
        plants[0] = PlantSeeds.Carrot;
        plants[1] = PlantSeeds.Cucumber;
        plants[2] = PlantSeeds.Tomato;
        currentPlant = plants[0];
    }

    void Update()
    {
        if(Input.GetKeyDown(_key))
        {
            if (plantMode == plants.Length)
            {
                plantMode = 0;
            }
            else
            {
                plantMode++;
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