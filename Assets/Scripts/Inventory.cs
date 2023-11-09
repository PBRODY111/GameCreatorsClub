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
    public Transform itemContent2;
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
        CleanList();
        ListItems();
    }

    public void Remove(InventoryItem item)
    {
        Items.Remove(item);
        CleanList();
        ListItems();
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
            //Hotbar
            GameObject obj = Instantiate(InventoryItem, itemContent);
            var itemName = obj.transform.Find("Name").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("Image").GetComponent<Image>();
            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
            //Inventory
            GameObject obj2 = Instantiate(InventoryItem, itemContent2);
            var itemName2 = obj2.transform.Find("Name").GetComponent<TMP_Text>();
            var itemIcon2 = obj2.transform.Find("Image").GetComponent<Image>();
            itemName2.text = item.itemName;
            itemIcon2.sprite = item.icon;
        }
        SetInventoryItems();
        
    }

    public void SetInventoryItems()
    {
        inventoryItems = itemContent2.GetComponentsInChildren<InventoryItemController>();
        for (int i = 0; i < Items.Count; i++)
        {
            inventoryItems[i].AddItem(Items[i]);
        }
    }
}
