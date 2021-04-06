using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {


    private Vector3 _growingDirection = new Vector3(0,1,0);

    [SerializeField] public float maxSize = 5f;
    
    public bool growingCondition = true;
    public bool readyToHarvest = false;
    public float growthRate = 0.5f;
    private float growthUpdateRate = 2f;
    
    void Start()
    {
        InvokeRepeating("Grow", growthUpdateRate, growthUpdateRate);
        // There is also "Cancel Invoke" might be helpful. But changing the growing condition is already enough I think
    }
    
        void Grow() {
            if (this.transform.localScale.y > maxSize)
            {
                growingCondition = false;
                readyToHarvest = true;
            }
            if (!growingCondition) return;
            
            this.transform.localScale += (growthRate * _growingDirection);
        }


}

