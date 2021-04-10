using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Plant : MonoBehaviour
{

    //basic parameters
    //private float humidity = 0f;
    //private bool watered = false;
    public int obstructiveWeeds = 0;
    public bool growingCondition = true;
    public bool readyToHarvest = false;
    //public float care = 0f;


    //plant specific parameters
    [SerializeField] public float maxSize = 3f;
    [SerializeField] public float GrowthRate = 2f;
    

    public float size = 0f;
    private SeedListener.PlantSeeds kindOfPlant;

    private Vector3 _growingDirection = new Vector3(0, 1, 0);
    //private float currentGrowthRate;
    //private float growthTickRate = 2f;
    //private float needed_care = 3f; //e.g how often we need to water, to harvest
    //private float health = 3f; //if health == 0 plant dies


    void Start()
    {
        StartCoroutine(checkIfDead());
       // kindOfPlant = SeedListener.getCurrentPlant();
        //InvokeRepeating("Grow", growthTickRate, growthTickRate);
        // There is also "Cancel Invoke" might be helpful. But changing the growing condition is already enough I think. Could also be replaced by a coroutine
    }
    

    public IEnumerator checkIfDead()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
        if (obstructiveWeeds == 7)
        {
            readyToHarvest = false;
            Destroy(this.gameObject);
        }
        }
    }
    

    //we first check the growing condition, it it is true then plant grows
    public void Grow()
    {
        //Weeds influence plant growth
        Debug.Log("in grow");
        if (obstructiveWeeds >= 4)
        {
            growingCondition = false;
            // plant changes color when sick
            this.GetComponent<MeshRenderer>().material.SetFloat("_Metallic", 0.5f);
        }
        else
        {
            if (size >= maxSize)
            {
                readyToHarvest = true;
                growingCondition = false;
            }
            else
            {
                growingCondition = true;
            }
            // plant default color
            this.GetComponent<MeshRenderer>().material.SetFloat("_Metallic", 0f);
        }

        
        if (!growingCondition) return;
        
        size++;
        Transform();
    }

    public void begin(SeedListener.PlantSeeds kind)
    {
        kindOfPlant = kind;
    }

    public SeedListener.PlantSeeds getKind()
    {
        return kindOfPlant;
    }

    //TODO: Transform depending on kind of Vegetable
    private void Transform()
    {
        Debug.Log("in transform");
        switch (kindOfPlant)
        {
            case SeedListener.PlantSeeds.Beet:
                if (size == 1)
                {
                    //this.gameObject.GetComponent<MeshFilter>().mesh = null;
                    
                    //plant = Instantiate(plants[0], transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity,
                       // this.transform);
                    
                    //transform
                   // this.transform.localScale += new Vector3(10f, 10f, 10f);
                   Debug.Log("Beet size 1");
                   this.transform.localScale = new Vector3(20f, 20f, 20f);
                } else if (size == 2)
                {
                    Debug.Log("Beet size 2");
                  
                   this.transform.localScale = new Vector3(10f, 22f, 10f);
                } else if (size == 3)
                {
           
                   this.transform.localScale = new Vector3(10f, 30f, 10f);
                }
                break;
            
            case SeedListener.PlantSeeds.Tomato:
                if (size == 1)
                {
                    //this.gameObject.GetComponent<MeshFilter>().mesh = null;
                    
                    //plant = Instantiate(plants[1], transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity,
                        //this.transform);
                    
                   // plant.transform.localScale = new Vector3(10f, 10f, 10f);
                } else if (size == 2)
                {
                   // plant.transform.localScale = new Vector3(15f, 15f, 15f);
                } else if (size == 3)
                {
                    //plant.transform.localScale = new Vector3(20f, 20f, 20f);
                }
                break;
            case SeedListener.PlantSeeds.Cabbage:
                if (size == 1)
                {
                    //this.gameObject.GetComponent<MeshFilter>().mesh = null;
                    
                    //plant = Instantiate(plants[2], transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity,
                        //this.transform);
                   
                    this.transform.localScale = new Vector3(10f, 10f, 10f);
                } else if (size == 2)
                {
                    //plant.transform.localScale = new Vector3(5f, 5f, 5f);
                } else if (size == 3)
                {
                    //plant.transform.localScale = new Vector3(20f, 20f, 20f);
                }
                break;
            default:
                //plant.transform.localScale = new Vector3(20f, 20f, 20f);
                break;
        }
        
    }




    /* public void Dry()
     {
         watered = false;
     }*/



}

