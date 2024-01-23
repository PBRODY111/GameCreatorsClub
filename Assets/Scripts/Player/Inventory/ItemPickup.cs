using UnityEngine;
using UnityEngine.Serialization;

namespace Player.Inventory
{
    public class ItemPickup : MonoBehaviour
    {
        [FormerlySerializedAs("Item")] public InventoryItem item;
        [SerializeField] private float reach;

        private void OnMouseDown()
        {
            if (IsWithinReach()) PickUp();
        }

        private void PickUp()
        {  
            if(Inventory.Instance.items.Count < 9){
                Inventory.Instance.Add(item);
                Destroy(gameObject);
            }
        }

        private bool IsWithinReach()
        {
            return Vector3.Distance(transform.position, Player.Instance.transform.position) <= reach;
        }
    }
}