using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Player.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance;
        [FormerlySerializedAs("Items")] public List<InventoryItem> items = new();

        public Transform itemContent;

        [FormerlySerializedAs("InventoryItem")]
        public GameObject inventoryItem;

        // Start is called before the first frame update
        private void Awake()
        {
            Instance = this;
        }

        public void Add(InventoryItem item)
        {
            items.Add(item);
            var obj = Instantiate(inventoryItem, itemContent);
            obj.transform.Find("Name").GetComponent<TMP_Text>().text = items.Count + ":" + item.itemName;
            obj.transform.Find("Image").GetComponent<Image>().sprite = item.icon;
            obj.GetComponent<InventoryItemController>().AddItem(item);
        }

        public void Remove(InventoryItem item)
        {
            items.Remove(item);
            CleanList();
            ListItems();
        }

        public void CleanList()
        {
            foreach (Transform item in itemContent) Destroy(item.gameObject);
        }

        public void ListItems()
        {
            var i = 0;
            foreach (var item in items)
            {
                i++;
                var obj = Instantiate(inventoryItem, itemContent);
                obj.transform.Find("Name").GetComponent<TMP_Text>().text = i + ":" + item.itemName;
                obj.transform.Find("Image").GetComponent<Image>().sprite = item.icon;
                obj.GetComponent<InventoryItemController>().AddItem(item);
            }
        }
    }
}