using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    public InventoryItem item;

    [FormerlySerializedAs("RemoveButton")] public Button removeButton;


    public void RemoveItem()
    {
        Inventory.Instance.Remove(item);
    }

    public void AddItem(InventoryItem newItem)
    {
        item = newItem;
        GetComponent<Image>().color = new Color(40 / 255f, 40 / 255f, 40 / 255f);
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
            transform.parent.GetChild(Player.Instance.selectedslot).GetComponent<InventoryItemController>()
                .ToggleColor();
            Player.Instance.selectedslot = transform.GetSiblingIndex();
        }
    }

    private void ToggleColor()
    {
        if (GetComponent<Image>().color == new Color(40 / 255f, 40 / 255f, 40 / 255f))
        {
            GetComponent<Image>().color = new Color(80 / 255f, 80 / 255f, 80 / 255f);
        }
        else
        {
            GetComponent<Image>().color = new Color(40 / 255f, 40 / 255f, 40 / 255f);
        }
    }

    public void UseItem()
    {
        Debug.Log("Using " + item.itemName);
        if (item.deselectOnUse)
            HoldItem();
        switch (item.itemType)
        {
            case InventoryItem.ItemType.Battery:
                if (Player.Instance.GetComponent<HeadLamp>().lightStage != 4)
                {
                    Player.Instance.GetComponent<HeadLamp>().Charge(5000);
                    HoldItem();
                    RemoveItem();
                }

                break;
            case InventoryItem.ItemType.Box:
                Instantiate(Player.Instance.stoolPrefab,
                    Player.Instance.transform.position + Player.Instance.transform.forward,
                    Quaternion.Euler(-90f, 0f, 0f));
                HoldItem();
                RemoveItem();
                break;
        }
    }
}