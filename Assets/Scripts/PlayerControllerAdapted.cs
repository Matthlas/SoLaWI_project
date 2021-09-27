using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class PlayerControllerAdapted : MonoBehaviour
{
    public enum Mode {
        Säen,
        Giessen,
        Jäten,
        Ernten,
        Buddeln

    }
    #region Private Members

    private Animator _animator;

    private CharacterController _characterController;

    private float Gravity = 20.0f;

    private Vector3 _moveDirection = Vector3.zero;

    private InventoryItemBase mCurrentItem = null;
    
    private List<InteractableItemBaseClass> InteractItemsList = new List<InteractableItemBaseClass>();
    
    private Mode mode;
    
    #endregion

    
    
    #region Public Members
    
    public GameObject Item;

    public float Speed = 5.0f;

    public float RotationSpeed = 240.0f;
    
    public GameObject Hand;

    public Transform cam;

    #endregion
    
    
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        mode = Mode.Giessen;
    }


     #region Inventory
    
     private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
     {
         InventoryItemBase item = e.Item;
    
         GameObject goItem = (item as MonoBehaviour).gameObject;
         goItem.SetActive(true);
         goItem.transform.parent = null;
         
         if (item == mCurrentItem)
             mCurrentItem = null;
    
     }
    
     private void SetItemActive(InventoryItemBase item, bool active)
     {
         GameObject currentItem = (item as MonoBehaviour).gameObject;
         currentItem.SetActive(active);
         currentItem.transform.parent = active ? Hand.transform : null;
     }

     
     private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
     {
         if (e.Item.ItemType != EItemType.Consumable)
         {
             // If the player carries an item, un-use it (remove from player's hand)
             if (mCurrentItem != null)
             {
                 SetItemActive(mCurrentItem, false);
             }
    
             InventoryItemBase item = e.Item;
    
             // Use item (put it to hand of the player)
             SetItemActive(item, true);
    
             mCurrentItem = e.Item;
         }
    
     }
    
     #endregion
     
    

    // Update is called once per frame
    void Update()
    {
        InteractWithItem();
        movePlayer();
    }

    public void InteractWithItem()
    {
        
        if (InteractItemsList.Count > 0 && Input.GetMouseButtonDown(0))
        {
            
            for (int i = 0; i < InteractItemsList.Count; i++)
            {
                // Interact animation 
                //InteractItemsList[i].OnInteractAnimation(_animator);
                switch (mode)
                {
                    case Mode.Säen:
                        _animator.SetTrigger("plant");
                        break;
                    case Mode.Giessen:
                        _animator.SetTrigger("water");
                        break;
                    case Mode.Jäten:
                        _animator.SetTrigger("weed");
                        break;
                    case Mode.Ernten:
                        _animator.SetTrigger("harvest");
                        break;
                    case Mode.Buddeln:
                        _animator.SetTrigger("weed");
                        break;
                }
                // Interaction function of the object
                InteractItemsList[i].OnInteract();
            }
        }
    }



    //if colliding wih interactable item show message
    private void OnTriggerEnter(Collider other)
    {
        TryInteraction(other);
    }

    private void TryInteraction(Collider other)
    {
        InteractableItemBaseClass item = other.GetComponent<InteractableItemBaseClass>();

        if (item != null)
        {
            if (item.CanInteract(other))
            {
               // Hud.OpenMessagePanel(item);
                item.Select();
                InteractItemsList.Add(item);
                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        InteractableItemBaseClass item = other.GetComponent<InteractableItemBaseClass>();
        if (item != null)
        {
            //Hud.CloseMessagePanel();
            InteractItemsList.Remove(item);
            item.Unselect();
        }
    }

    private void movePlayer()
    
    {
        // Get Input for axis
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Calculate the forward vector
        Vector3 camForward_Dir = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 move = v * camForward_Dir + h * cam.transform.right;

        if (move.magnitude > 1f) move.Normalize();


        // Calculate the rotation for the player
        move = transform.InverseTransformDirection(move);

        // Get Euler angles
        float turnAmount = Mathf.Atan2(move.x, move.z);

        transform.Rotate(0, turnAmount * RotationSpeed * Time.deltaTime, 0);

        if (_characterController.isGrounded)
        {
            _moveDirection = transform.forward * move.magnitude;
            _moveDirection *= Speed;
            _animator.SetBool("run", move.magnitude > 0);
        }
        else
        {
            Gravity = 20.0f;
        }


        _moveDirection.y -= Gravity * Time.deltaTime;
        
        

        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    //change player mode according to selected iventory item and put item in players hand
    public void setMode(KeyCode key)
    {
        
        if (key.Equals(KeyCode.Alpha1))
        {
            mode = Mode.Säen;

            Sprite picture =  GameObject.FindGameObjectWithTag("Slot1").GetComponent<Image>().sprite;
            
            Hand.GetComponent<SpriteRenderer>().sprite = picture;
            
            Item.transform.localPosition = new Vector3(-0.000280000007f,0.000509999983f,0.00368000008f); 
            Item.transform.localEulerAngles = new Vector3(10.1638374f,82.7932434f,133.340912f);
            Item.transform.localScale = new Vector3(0.0224425513f,0.0224425513f,0.0224425513f);
        }
        else if (key.Equals(KeyCode.Alpha2))
        {
            mode = Mode.Giessen;
            
            Sprite picture =  GameObject.FindGameObjectWithTag("Slot2").GetComponent<Image>().sprite;
            
            Hand.GetComponent<SpriteRenderer>().sprite = picture;
            
            Item.transform.localPosition = new Vector3(0.0006f, 0.00326f, 0.00059f);
            Item.transform.localEulerAngles = new Vector3(10.164f,82.793f, 145.928f);
            Item.transform.localScale = new Vector3(0.001258271f, 0.001258271f, 0.001258271f);
           
        }
        else if (key.Equals(KeyCode.Alpha3))
        {
            mode = Mode.Jäten;
            Sprite picture =  GameObject.FindGameObjectWithTag("Slot3").GetComponent<Image>().sprite;
            
            Hand.GetComponent<SpriteRenderer>().sprite = picture;
            
            
            Item.transform.localPosition = new Vector3(-8e-05f, 0.00083f, 0.00252f);
            Item.transform.localEulerAngles = new Vector3(10.164f,82.793f, 316.7f);
            Item.transform.localScale = new Vector3(0.001778063f, 0.001778063f, 0.001778063f);
        }
        else if (key.Equals(KeyCode.Alpha4))
        {
            mode = Mode.Ernten;
            Sprite picture =  GameObject.FindGameObjectWithTag("Slot4").GetComponent<Image>().sprite;
            
            Hand.GetComponent<SpriteRenderer>().sprite = picture;
            
            Item.transform.localPosition = new Vector3(0.000300000014f,0.00236999989f,0.00173999998f);
            Item.transform.localEulerAngles = new Vector3(10.1638374f,82.7932434f,133.340912f);
            Item.transform.localScale = new Vector3(0.00103073695f,0.00103073695f,0.00103073695f);
            
            
        }
        else if (key.Equals(KeyCode.Alpha5))
        {
            mode = Mode.Buddeln;
            Sprite picture =  GameObject.FindGameObjectWithTag("Slot5").GetComponent<Image>().sprite;
            
            Hand.GetComponent<SpriteRenderer>().sprite = picture;
            
            Item.transform.localPosition = new Vector3(-9e-05f, 0.00048f, 0.00212f);
            Item.transform.localEulerAngles = new Vector3(10.164f,82.793f, 133.341f);
            Item.transform.localScale = new Vector3(0.004096563f, 0.004096563f, 0.004096563f);
        }
    }

    public Mode getMode()
    {
        return mode;
    }
}
