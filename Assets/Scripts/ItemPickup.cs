using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventoryItem Item;
    [SerializeField] private GameObject intText2;
    [SerializeField] private float reach;

    void PickUp()
    {
        Inventory.Instance.Add(Item);
        Destroy(gameObject);
    }

    void OnMouseOver()
    {
        intText2.SetActive(IsWithinReach());
    }

    private void OnMouseDown()
    {
        if(IsWithinReach()){
            PickUp();
            intText2.SetActive(false);
        }
    }
    void OnMouseExit()
    {
        intText2.SetActive(false);
    }
    bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position) <= reach;
    }
}
