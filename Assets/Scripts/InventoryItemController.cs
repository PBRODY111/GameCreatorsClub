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
        Destroy(gameObject);
    }

    public void AddItem(InventoryItem newItem)
    {
        Debug.Log("Item Added to "+gameObject.name);
        item = newItem;
    }

    public void UseItem()
    {
        switch (item.itemType)
        {
            case InventoryItem.ItemType.Battery:
                Debug.Log("charging +1000");
                Player.Instance.GetComponent<HeadLamp>().Charge(1000);
                break;

        }
    }
}
