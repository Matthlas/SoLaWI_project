using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClickHandler : MonoBehaviour
{
    public Inventory _Inventory;

    public KeyCode _Key;

    private Button _button;

    void Awake()
    {
        _button = GetComponent<Button>();
    }

    void Update()
    {
        if(Input.GetKeyDown(_Key))
        {
            FadeToColor(_button.colors.pressedColor);

            OnItemClicked();
            // Click the button
            //_button.onClick.Invoke();
            
            
        }
        else if(Input.GetKeyUp(_Key))
        {
            FadeToColor(_button.colors.normalColor);
        }
    }

    void FadeToColor(Color color)
    {
        Graphic graphic = GetComponent<Graphic>();
        graphic.CrossFadeColor(color, _button.colors.fadeDuration, true, true);
    }

    private InventoryItemBase AttachedItem
    {
        get
        {
            ItemDragHandler dragHandler =
            gameObject.transform.Find("ItemImage").GetComponent<ItemDragHandler>();

            return dragHandler.Item;
        }
    }

    public void OnItemClicked()
    {
        Debug.Log("yip");
        GameObject.Find("Player").GetComponent<PlayerControllerAdapted>().setMode(_Key);
        /*InventoryItemBase item = AttachedItem;

        if (item != null)
        {
            _Inventory.UseItem(item);
        }*/
    }

}
