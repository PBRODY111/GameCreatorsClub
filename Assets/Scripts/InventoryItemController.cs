using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    public InventoryItem item;

    public Button RemoveButton;


    public void RemoveItem()
    {
        Inventory.Instance.Remove(item);
    }

    public void AddItem(InventoryItem newItem)
    {
        item = newItem;
    }

    public void UseItem()
    {
        Debug.Log("Using " + item.itemName);
        switch (item.itemType)
        {
            case InventoryItem.ItemType.Battery:
                if(Player.Instance.GetComponent<HeadLamp>()._lightStage != 4){
                    Player.Instance.GetComponent<HeadLamp>().Charge(5000);
                    RemoveItem();
                    
                }
                break;
            case InventoryItem.ItemType.Box:
                Debug.Log("Should be ");
                Instantiate(Player.Instance._stoolPrefab,Player.Instance.transform.position + Player.Instance.transform.forward,Quaternion.Euler(-90f, 0f, 0f));
                RemoveItem();
                break;
        }
    }
}
