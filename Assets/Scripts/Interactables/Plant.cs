using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : InteractableItemBaseClass {

    
    [SerializeField]
    private GameObject _plantPrefab;

    [SerializeField] 
    private Vector3 growthRate;
    //_plantPrefab = (GameObject)Instantiate(Resources.Load("Plant"));
    
    
    [SerializeField] 
    public float interactionDelay = 20;
    private bool isPlanted = false;
    private float lastInteractionTime = 0f;
    
    //finite state machine for bed
    
    public void Planting()
        {
            Debug.Log("Planting");
            //GameObject plant = Instantiate(plant, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity, this.transform);
            
            //Instantiate(_plantPrefab, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity, this.transform);
            
            //_interactionCue.Play();
            
        }
        

        public void Watering()
                {
                    Debug.Log("Watering");
                    _plantPrefab.transform.localScale += growthRate;
                    _interactionCue.Play();
                }
        
        public void Harvesting()
        {
            Debug.Log("Harvesting");
            //Destroy(current_plant);
            _interactionCue.Play();
        }
        

}


