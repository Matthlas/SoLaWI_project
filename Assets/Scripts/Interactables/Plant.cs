using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    //basic parameters
    private float humidity = 0f;
    private bool watered = false;
    public int obstructiveWeeds = 0;
    public bool growingCondition = true; //Perhaps unnecessary
    public bool readyToHarvest = false;
    public float care = 0f;


    //plant specific parameters
    [SerializeField] public float maxSize = 6f;
    [SerializeField] public float BaseGrowthRate = 0.1f;

    private Vector3 _growingDirection = new Vector3(0, 1, 0);
    private float currentGrowthRate;
    private float growthTickRate = 2f;
    private float needed_care = 3f; //e.g how often we need to water, to harvest
    private float health = 3f; //if health == 0 plant dies


    void Start()
    {
        currentGrowthRate = BaseGrowthRate;
        InvokeRepeating("Grow", growthTickRate, growthTickRate);
        // There is also "Cancel Invoke" might be helpful. But changing the growing condition is already enough I think. Could also be replaced by a coroutine
    }

    void Grow()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        //Only grow until max size
        if (this.transform.localScale.y >= maxSize)
        {
            growingCondition = false;
            readyToHarvest = true;
        }

        //Weeds influence plant growth
        if (obstructiveWeeds == 5)
        {
            currentGrowthRate = 0;
            health -= 1;
        }

        if (obstructiveWeeds >= 10)
        {
            health = 0;
        }

        if (!growingCondition) return;
        //if (this.transform.localScale.y <= ((maxSize / needed_care) * care)) // n deckel aufs wachsen draufpacken, wenn nicht genug care raufpacken

        this.transform.localScale += (currentGrowthRate * _growingDirection);
    }





    public void Water()
    {
        if (watered)
        {
            //overwatering
            health -= 1;
        }
        else
        {
            watered = true;
            currentGrowthRate *= 4;
            care += 1;

        }

        
    }
    
    public void Dry()
    {
        watered = false;
        currentGrowthRate = BaseGrowthRate;
    }


    
}

