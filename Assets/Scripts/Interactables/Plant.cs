using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Plant : MonoBehaviour
{

    //plant customizable parameters
    [SerializeField] public Vector3 targetSize = new Vector3(4f, 4f, 4f);
    [SerializeField] public int growth_time = 20;
    [SerializeField] public ParticleSystem _harvestCue;
    
    //basic parameters
    private bool watered = false;
    internal int obstructiveWeeds = 0;
    private bool growingCondition = true;
    internal bool readyToHarvest = false;
    
    private SeedListener.PlantSeeds kindOfPlant;

    private float growthTickRate = 2f;
    private int target_growth_ticks;
    private int ticks_elapsed = 0;
    private Vector3 startSize;

    private Color healthyColor = new Color(1f, 1f, 1f, 1f);
    private Color sickColor = new Color(1f, 0.3f, 0.3f, 1f);
                                      

    void Start()
    {
        startSize = this.transform.localScale;
        target_growth_ticks = growth_time / (int)growthTickRate;
        //Calculate target scale with respect to parent scale
        targetSize = new Vector3(targetSize.x, targetSize.y * 1f /this.transform.parent.transform.localScale.y, targetSize.z);
        
        // kindOfPlant = SeedListener.getCurrentPlant();
        InvokeRepeating("CheckGrowth", growthTickRate, growthTickRate);
        Debug.Log(gameObject.GetComponent<Renderer>().material.color);
    }
    

    //Plant checks whether it grows every 2 seconds
    public void CheckGrowth()
    {

        //If the plant reached its max_size it stops growing
        if (ticks_elapsed == target_growth_ticks)
        {
            Ripen();
        }
        else
        {
            if (obstructiveWeeds < 3)
            {
                growingCondition = true;
                gameObject.GetComponent<Renderer>().material.color = healthyColor;
            }
            // Plant falls sick if there are not too many weeds
            else if (obstructiveWeeds >= 3)
            {
                growingCondition = false;
                // plant changes color when sick
                gameObject.GetComponent<Renderer>().material.color = sickColor;
            }
            // Plant dies if there are too many weeds
            if (obstructiveWeeds >= 4)
            {
                Destroy(this.gameObject);
            }
            
            // If the plant has water and everything is fine it grows
            if(watered && growingCondition)
            {
                ticks_elapsed++;
                this.transform.localScale = Vector3.Lerp(startSize, targetSize, (float)ticks_elapsed / (float)target_growth_ticks);
            }
        }

    }

    public void begin(SeedListener.PlantSeeds kind)
    {
        kindOfPlant = kind;
    }

    public SeedListener.PlantSeeds getKind()
    {
        return kindOfPlant;
    }


    public void Water()
    {
        watered = true;
    }

     public void Dry()
     {
         watered = false;
     }
     


     private void Ripen()
     {
         readyToHarvest = true;
         growingCondition = false;
         CancelInvoke("CheckGrowth");
         Instantiate(_harvestCue, transform.position, Quaternion.identity, this.transform); 
     }

}

