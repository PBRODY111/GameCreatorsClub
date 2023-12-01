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

    public void HoldItem()
    {
        ToggleColor();
        if (Player.Instance.selectedslot == -1)
        {
            Player.Instance.selectedslot = transform.GetSiblingIndex();
        }
        else if (Player.Instance.selectedslot == transform.GetSiblingIndex())
        {
            Player.Instance.selectedslot = -1;
        }
        else
        {
            transform.parent.GetChild(Player.Instance.selectedslot).GetComponent<InventoryItemController>().ToggleColor();
            Player.Instance.selectedslot = transform.GetSiblingIndex();
        }
            
        
        
    }

    private void ToggleColor()
    {
        if (GetComponent<Image>().color == new Color(1f, 1f, 1f))
            GetComponent<Image>().color = new Color(20 / 255f, 50 / 255f, 20 / 255f);
        else
            GetComponent<Image>().color = new Color(1f, 1f, 1f);
    }
    public void UseItem()
    {
        Debug.Log("Using " + item.itemName);
        if(item.deselectOnUse)
            HoldItem();
        switch (item.itemType)
        {
            case InventoryItem.ItemType.Battery:
                if(Player.Instance.GetComponent<HeadLamp>()._lightStage != 4){
                    Player.Instance.GetComponent<HeadLamp>().Charge(5000);
                    RemoveItem();
                    
                }
                break;
            case InventoryItem.ItemType.Box:
                Instantiate(Player.Instance.stoolPrefab,Player.Instance.transform.position + Player.Instance.transform.forward,Quaternion.Euler(-90f, 0f, 0f));
                RemoveItem();
                break;
        }
    }
}
