using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;
using UnityEngine;
using Player.Inventory;

public class Cannon : MonoBehaviour
{
    [FormerlySerializedAs("Item")] public InventoryItem item;
    [SerializeField] private float reach;
    [SerializeField] private GameObject armCannon;
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        if (IsWithinReach()){
            armCannon.SetActive(true);
            PickUp();
        }
    }

    private void PickUp()
    {  
        Inventory.Instance.Add(item);
        Destroy(gameObject);
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
