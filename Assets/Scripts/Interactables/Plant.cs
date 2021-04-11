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
    [SerializeField] public float GrowthRate;
    

    public float size = 1f;
    private SeedListener.PlantSeeds kindOfPlant;

    private Vector3 _growingDirection = new Vector3(0, 1, 0);
    
    //private float growthTickRate = 2f;
    
    


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
        
        if (size >= maxSize)
        {
            readyToHarvest = true;
            growingCondition = false;
        }
        else
        {
            if (obstructiveWeeds >= 4)
            {
                growingCondition = false;
                // plant changes color when sick
                this.GetComponent<MeshRenderer>().material.SetFloat("_Metallic", 0.5f);
            }
            else
            {
                growingCondition = true;
                size++;
                Transform();
            }

        

        // plant default color
        this.GetComponent<MeshRenderer>().material.SetFloat("_Metallic", 0f);
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

    //TODO: Transform depending on kind of Vegetable
    private void Transform()
    {
        this.transform.localScale = new Vector3(transform.localScale.x * GrowthRate, transform.localScale.y * GrowthRate, transform.localScale.z * GrowthRate);

    }




    /* public void Dry()
     {
         watered = false;
     }*/



}

