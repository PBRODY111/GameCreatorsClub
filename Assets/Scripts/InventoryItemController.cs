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
        switch (item.itemType)
        {
            case InventoryItem.ItemType.Battery:
                Player.Instance.GetComponent<HeadLamp>().Charge(1000);
                RemoveItem();
                break;
        }
    }
}
