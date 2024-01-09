using UnityEngine;
using System;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [FormerlySerializedAs("_hotbar")] [SerializeField] private GameObject hotbar;
    public Camera mainCamera;
    public GameObject stoolPrefab;
    public int selectedslot;

    private void Awake()
    {
        Instance = this;
        selectedslot = -1;
    }

    public InventoryItem GetHeldItem()
    {
        return hotbar.transform.GetChild(selectedslot).GetComponent<InventoryItemController>().item;
    }

    public bool EpicModeEnabled()
    {
        return GetComponent<PlayerMovement>().epicModeEnabled;
    }

    private void Update()
    {
        try
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Debug.Log(
                    hotbar.transform.GetChild(selectedslot).GetComponent<InventoryItemController>().item.itemName);
                hotbar.transform.GetChild(selectedslot).GetComponent<InventoryItemController>().UseItem();
            }

            for (var i = 0; i < 10; i++)
            {
                var key = (KeyCode)Enum.Parse(typeof(KeyCode), "Alpha" + (i == 9 ? 0 : i + 1));
                if (Input.GetKeyDown(key))
                {
                    hotbar.transform.GetChild(i).GetComponent<InventoryItemController>().HoldItem();
                    break;
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log("No Item in Slot" + e);
        }
    }
}