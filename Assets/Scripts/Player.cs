using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] private GameObject _hotbar;
    public Camera mainCamera;
    public GameObject stoolPrefab;
    public int selectedslot;
    void Awake(){
        Instance = this;
        selectedslot = -1;
    }

    public InventoryItem GetHeldItem()
    {
        return _hotbar.transform.GetChild(selectedslot).GetComponent<InventoryItemController>().item;
    }

    public bool EpicModeEnabled()
    {
        return GetComponent<PlayerMovement>().epicModeEnabled;
    }

    void Update()
    {
        
        try
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Debug.Log(_hotbar.transform.GetChild(selectedslot).GetComponent<InventoryItemController>().item.itemName);
                _hotbar.transform.GetChild(selectedslot).GetComponent<InventoryItemController>().UseItem();
                
            }

            for (int i = 0; i < 10; i++)
            {
                KeyCode key = (KeyCode)Enum.Parse(typeof(KeyCode), "Alpha" + (i == 9 ? 0 : i + 1));
                if (Input.GetKeyDown(key))
                {
                    _hotbar.transform.GetChild(i).GetComponent<InventoryItemController>().HoldItem();
                    break;
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("No Item in Slot" + e);
        }
       
    }

}
