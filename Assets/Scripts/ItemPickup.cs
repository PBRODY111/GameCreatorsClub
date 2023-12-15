using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventoryItem Item;
    [SerializeField] private float reach;

    void PickUp()
    {
        Inventory.Instance.Add(Item);
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        if(IsWithinReach()){
            PickUp();
        }
    }
    bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position) <= reach;
    }
}
