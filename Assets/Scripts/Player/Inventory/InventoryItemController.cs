using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Player.Inventory
{
    public class InventoryItemController : MonoBehaviour
    {
        public InventoryItem item;

        [FormerlySerializedAs("RemoveButton")] public Button removeButton;
        
        private readonly Color _defaultColor = new Color(40 / 255f, 40 / 255f, 40 / 255f);
        private readonly Color _selectedColor = new Color(80 / 255f, 80 / 255f, 80 / 255f);

        public void RemoveItem()
        {
            Inventory.Instance.Remove(item);
        }

        public void AddItem(InventoryItem newItem)
        {
            item = newItem;
            GetComponent<Image>().color = _defaultColor;
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

            UpdateInstructions();
        }
        
        public void UpdateInstructions()
        {
            if (Player.Instance.selectedslot != -1) Inventory.Instance.usageInstructions.text = "Right Click to use item\nPress Q to drop item";
            else Inventory.Instance.usageInstructions.text = "";
        }

        public void ToggleColor()
        {
            var imag = GetComponent<Image>();
            imag.color = imag.color == _defaultColor ? _selectedColor : _defaultColor;
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
                    Instantiate(Player.Instance.stoolPrefab, Player.Instance.transform.position + Player.Instance.transform.forward, Quaternion.Euler(-90f, 0f, 0f));
                    HoldItem();
                    RemoveItem();
                    break;
                case InventoryItem.ItemType.Ladder:
                    Instantiate(Player.Instance.ladderPrefab, Player.Instance.transform.position + Player.Instance.transform.forward, Quaternion.Euler(0f, 180f, 0f));
                    HoldItem();
                    RemoveItem();
                    break;
            }
        }

        public void DropItem()
        {
            if (item.modelPrefab != null)
            {
                
                // Ensure a valid InventoryItemController is found
                if (item.modelPrefab != null)
                {
                    // Drop the model of the selected item
                    Instantiate(item.modelPrefab, Player.Instance.transform.position + Player.Instance.transform.forward * 1.2f, Quaternion.identity);
                    RemoveItem();
                    Player.Instance.selectedslot = -1;
                }
            }

            UpdateInstructions();
        }
    }
}
