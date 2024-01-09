using UnityEngine;
using UnityEngine.Serialization;

public class ItemPickup : MonoBehaviour
{
    [FormerlySerializedAs("Item")] public InventoryItem item;
    [SerializeField] private float reach;

    private void PickUp()
    {
        Inventory.Instance.Add(item);
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        if (IsWithinReach())
        {
            PickUp();
        }
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position) <= reach;
    }
}