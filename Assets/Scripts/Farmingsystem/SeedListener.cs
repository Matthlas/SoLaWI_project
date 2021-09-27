using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UIElements;

public class SeedListener : MonoBehaviour
{
    [SerializeField]
    public KeyCode _key;

    [SerializeField]
    public Sprite[] Images = new Sprite[3];
    
    public enum PlantSeeds
    {
        Cabbage,
        Tomato,
        Beet
    }

    private PlantSeeds[] plants = new PlantSeeds[3];
    
    //index of plant
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
            //skip back to first plant after last
            if (plantMode == plants.Length-1)
            {
                plantMode = 0;

            }
            else
            {
                ++plantMode;
            }

            currentPlant = plants[plantMode];
            StartCoroutine(showCurrentPlant());
            

        }
    }

    private IEnumerator showCurrentPlant()
    {
        //enable all images
       foreach (var image in gameObject.GetComponentsInChildren<UnityEngine.UI.Image>())
       {
           image.enabled = true;
       }
       //change ItemImage
       GameObject.FindGameObjectWithTag("ItsMe!").GetComponent<UnityEngine.UI.Image>().sprite = 
           Images[plantMode];
       
       yield return new WaitForSeconds(2);
       //disable all images
       foreach (var image in gameObject.GetComponentsInChildren<UnityEngine.UI.Image>())
       {
           image.enabled = false;
       }
    }
    

    public static PlantSeeds getCurrentPlant()
    {
        return currentPlant;
    }
    
}
