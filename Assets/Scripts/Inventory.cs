using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public List<InventoryItem> Items = new List<InventoryItem>();

    public Transform itemContent;
    public GameObject InventoryItem;

    public InventoryItemController[] inventoryItems;

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

    public void Add(InventoryItem item)
    {
        Items.Add(item);
        Debug.Log("Added Item: "+item.itemName);
    }

    public void Remove(InventoryItem item)
    {
        Items.Remove(item);
    }

    public void CleanList()
    {
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }
    }

    public void ListItems()
    {
        CleanList();
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, itemContent);
            var itemName = obj.transform.Find("Name").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("Image").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }
        SetInventoryItems();
        
    }

    public void SetInventoryItems()
    {
        inventoryItems = itemContent.GetComponentsInChildren<InventoryItemController>();
        for (int i = 0; i < Items.Count; i++)
        {
            inventoryItems[i].AddItem(Items[i]);
        }
    }
}
