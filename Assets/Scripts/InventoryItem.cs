using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (fileName = "New Item", menuName = "InventoryItem/Create New Item")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public ItemType itemType;
    public bool deselectOnUse;
    public enum ItemType
    {
        Misc,
        Battery,
        Box,
        Pick,
        Screwdriver
    };
}
