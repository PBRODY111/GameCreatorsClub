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

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

    public void Add(InventoryItem item)
    {
        Items.Add(item);
        GameObject obj = Instantiate(InventoryItem, itemContent);
        obj.transform.Find("Name").GetComponent<TMP_Text>().text = Items.Count+":"+item.itemName;
        obj.transform.Find("Image").GetComponent<Image>().sprite = item.icon;
        obj.GetComponent<InventoryItemController>().AddItem(item);
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
        int i = 0;
        foreach (var item in Items)
        {
            i++;
            GameObject obj = Instantiate(InventoryItem, itemContent);
            obj.transform.Find("Name").GetComponent<TMP_Text>().text = i+":"+item.itemName;
            obj.transform.Find("Image").GetComponent<Image>().sprite = item.icon;
            obj.GetComponent<InventoryItemController>().AddItem(item);
        }
    }
}
