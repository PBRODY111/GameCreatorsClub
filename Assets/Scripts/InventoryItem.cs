using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "InventoryItem/Create New Item")]
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
        Ladder,
        Pick,
        Screwdriver,
        Crowbar,
        Screws,
        Poles,
        Mask
    };
}