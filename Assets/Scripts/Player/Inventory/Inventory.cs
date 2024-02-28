using System;
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

        [SerializeField] public TMP_Text usageInstructions;
        [SerializeField] private TMP_Text denyText;
        [SerializeField] private AudioSource denySound;
        private void Awake()
        {
            Instance = this;

            usageInstructions.text = "";
        }

        public void Add(InventoryItem item)
        {
            items.Add(item);
            var obj = Instantiate(inventoryItem, itemContent);
            obj.transform.Find("Name").GetComponent<TMP_Text>().text = item.itemName;
            obj.transform.Find("Image").GetComponent<Image>().sprite = item.icon;
            obj.transform.Find("Num").GetComponent<TMP_Text>().text = items.Count.ToString();
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
                obj.transform.Find("Name").GetComponent<TMP_Text>().text = item.itemName;
                obj.transform.Find("Image").GetComponent<Image>().sprite = item.icon;
                obj.transform.Find("Num").GetComponent<TMP_Text>().text = i.ToString();
                obj.GetComponent<InventoryItemController>().AddItem(item);
            }
        }
        
        public void ActionDenied(string message)
        {
            denyText.text = message;
            denySound.Play();
            Invoke(nameof(ClearDenyText), 2);
        }
        
        private void ClearDenyText() => denyText.text = "";
    }
}