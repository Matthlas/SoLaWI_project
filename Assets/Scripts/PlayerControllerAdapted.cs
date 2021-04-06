using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerControllerAdapted : MonoBehaviour
{
    #region Private Members

    private Animator _animator;

    private CharacterController _characterController;

    private float Gravity = 20.0f;

    private Vector3 _moveDirection = Vector3.zero;

    private InteractableItemBaseClass mCurrentItem = null;

    // private HealthBar mHealthBar;

    // private HealthBar mFoodBar;

    // private int startHealth;

    // private int startFood;

    // private bool mCanTakeDamage = true;

    #endregion
    private InteractableItemBaseClass mInteractItem = null;
    private List<InteractableItemBaseClass> InteractItemsList = new List<InteractableItemBaseClass>();

    #region Public Members

    public float Speed = 5.0f;

    public float RotationSpeed = 240.0f;

    public Inventory Inventory;

    public GameObject Hand;

    public HUD_ours Hud;

    public float JumpSpeed = 7.0f;

    // public event EventHandler PlayerDied;

    public Transform cam;

    #endregion
    

    // public UnityEvent QuestCompleted;

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        // Inventory.ItemUsed += Inventory_ItemUsed;
        // Inventory.ItemRemoved += Inventory_ItemRemoved;

        // mHealthBar = Hud.transform.Find("Bars_Panel/HealthBar").GetComponent<HealthBar>();
        // mHealthBar.Min = 0;
        // mHealthBar.Max = Health;
        // startHealth = Health;
        // mHealthBar.SetValue(Health);

        // mFoodBar = Hud.transform.Find("Bars_Panel/FoodBar").GetComponent<HealthBar>();
        // mFoodBar.Min = 0;
        // mFoodBar.Max = Food;
        // startFood = Food;
        // mFoodBar.SetValue(Food);
        //
        // InvokeRepeating("IncreaseHunger", 0, HungerRate);
    }


    // #region Inventory
    //
    // private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    // {
    //     InventoryItemBase item = e.Item;
    //
    //     GameObject goItem = (item as MonoBehaviour).gameObject;
    //     goItem.SetActive(true);
    //     goItem.transform.parent = null;
    //
    //     if (item == mCurrentItem)
    //         mCurrentItem = null;
    //
    // }
    //
    // private void SetItemActive(InventoryItemBase item, bool active)
    // {
    //     GameObject currentItem = (item as MonoBehaviour).gameObject;
    //     currentItem.SetActive(active);
    //     currentItem.transform.parent = active ? Hand.transform : null;
    // }
    //
    //
    // private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    // {
    //     if (e.Item.ItemType != EItemType.Consumable)
    //     {
    //         // If the player carries an item, un-use it (remove from player's hand)
    //         if (mCurrentItem != null)
    //         {
    //             SetItemActive(mCurrentItem, false);
    //         }
    //
    //         InventoryItemBase item = e.Item;
    //
    //         // Use item (put it to hand of the player)
    //         SetItemActive(item, true);
    //
    //         mCurrentItem = e.Item;
    //     }
    //
    // }
    //
    // private int Attack_1_Hash = Animator.StringToHash("Base Layer.Attack_1");
    //
    // public bool IsAttacking
    // {
    //     get
    //     {
    //         AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
    //         if (stateInfo.fullPathHash == Attack_1_Hash)
    //         {
    //             return true;
    //         }
    //         return false;
    //     }
    // }
    //
    // public void DropCurrentItem()
    // {
    //     mCanTakeDamage = false;
    //
    //     _animator.SetTrigger("tr_drop");
    //
    //     GameObject goItem = (mCurrentItem as MonoBehaviour).gameObject;
    //
    //     Inventory.RemoveItem(mCurrentItem);
    //
    //     // Throw animation
    //     Rigidbody rbItem = goItem.AddComponent<Rigidbody>();
    //     if (rbItem != null)
    //     {
    //         rbItem.AddForce(transform.forward * 2.0f, ForceMode.Impulse);
    //
    //         Invoke("DoDropItem", 0.25f);
    //     }
    // }
    //
    // public void DropAndDestroyCurrentItem()
    // {
    //     GameObject goItem = (mCurrentItem as MonoBehaviour).gameObject;
    //
    //     Inventory.RemoveItem(mCurrentItem);
    //
    //     Destroy(goItem);
    //
    //     mCurrentItem = null;
    // }
    //
    // public void DoDropItem()
    // {
    //     mCanTakeDamage = true;
    //     if (mCurrentItem != null)
    //     {
    //         // Remove Rigidbody
    //         Destroy((mCurrentItem as MonoBehaviour).GetComponent<Rigidbody>());
    //
    //         mCurrentItem = null;
    //
    //         mCanTakeDamage = true;
    //     }
    // }
    //
    // #endregion

    // #region Health & Hunger
    //
    // [Tooltip("Amount of health")]
    // public int Health = 100;
    //
    // [Tooltip("Amount of food")]
    // public int Food = 100;
    //
    // [Tooltip("Rate in seconds in which the hunger increases")]
    // public float HungerRate = 0.5f;
    //
    // public void IncreaseHunger()
    // {
    //     Food--;
    //     if (Food < 0)
    //         Food = 0;
    //
    //     mFoodBar.SetValue(Food);
    //
    //     if (Food == 0)
    //     {
    //         CancelInvoke();
    //         Die();
    //     }
    // }
    //
    // public bool IsDead = true;
    // {
    //     get
    //     {
    //         return Health == 0 || Food == 0;
    //     }
    // }
    //
    // public bool CarriesItem(string itemName)
    // {
    //     if (mCurrentItem == null)
    //         return false;
    //
    //     return (mCurrentItem.Name == itemName);
    // }

    // public InventoryItemBase GetCurrentItem()
    // {
    //     return mCurrentItem;
    // }

    // public bool IsArmed
    // {
    //     get
    //     {
    //         if (mCurrentItem == null)
    //             return false;
    //
    //         return mCurrentItem.ItemType == EItemType.Weapon;
    //     }
    // }
    //
    //
    // public void Eat(int amount)
    // {
    //     Food += amount;
    //     if (Food > startFood)
    //     {
    //         Food = startFood;
    //     }
    //
    //     mFoodBar.SetValue(Food);
    //
    // }
    //
    // public void Rehab(int amount)
    // {
    //     Health += amount;
    //     if (Health > startHealth)
    //     {
    //         Health = startHealth;
    //     }
    //
    //     mHealthBar.SetValue(Health);
    // }
    //
    // public void TakeDamage(int amount)
    // {
    //     if (!mCanTakeDamage)
    //         return;
    //
    //     Health -= amount;
    //     if (Health < 0)
    //         Health = 0;
    //
    //     mHealthBar.SetValue(Health);
    //
    //     if (IsDead)
    //     {
    //         Die();
    //     }
    //
    // }
    //
    //
    // private void Die()
    // {
    //     _animator.SetTrigger("death");
    //
    //     if (PlayerDied != null)
    //     {
    //         PlayerDied(this, EventArgs.Empty);
    //     }
    // }
    //
    // #endregion


    // public void Talk()
    // {
    //     _animator.SetTrigger("tr_talk");
    // }

    // private bool mIsControlEnabled = true;
    //
    // public void EnableControl()
    // {
    //     mIsControlEnabled = true;
    // }
    //
    // public void DisableControl()
    // {
    //     mIsControlEnabled = false;
    // }
    

    // void FixedUpdate()
    // {
    //     if (!IsDead)
    //     {
    //         // Drop item
    //         if (mCurrentItem != null && Input.GetKeyDown(KeyCode.R))
    //         {
    //             DropCurrentItem();
    //         }
    //     }
    // }
    

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
                InteractItemsList[i].OnInteractAnimation(_animator);
                // Interaction function of the object
                InteractItemsList[i].OnInteract();
            }


            // if (mInteractItem is InventoryItemBase)
            // {
            //     InventoryItemBase inventoryItem = mInteractItem as InventoryItemBase;
            //     Inventory.AddItem(inventoryItem);
            //     inventoryItem.OnPickup();
            //
            //     if (inventoryItem.UseItemAfterPickup)
            //     {
            //         Inventory.UseItem(inventoryItem);
            //     }
            //     Hud.CloseMessagePanel();
            //     mInteractItem = null;
            // }
            //else
            //{
            //    if (mInteractItem.ContinueInteract())
            //    {
            //        Hud.OpenMessagePanel(mInteractItem);
            //    }
            //    else
            //    {
            //        Hud.CloseMessagePanel();
            //        mInteractItem = null;
            //    }
            //}
        }
    }



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
                Hud.OpenMessagePanel(item);
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
            Hud.CloseMessagePanel();
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

            if (Input.GetButton("Jump"))
            {
                _animator.SetBool("is_in_air", true);
                _moveDirection.y = JumpSpeed;

            }
            else
            {
                _animator.SetBool("is_in_air", false);
                _animator.SetBool("run", move.magnitude > 0);
            }
        }
        else
        {
            Gravity = 20.0f;
        }


        _moveDirection.y -= Gravity * Time.deltaTime;

        _characterController.Move(_moveDirection * Time.deltaTime);
    }
}
