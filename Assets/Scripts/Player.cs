using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] private GameObject _hotbar;
    void Awake(){
        Instance = this;
    }

    void Update()
    {
        try
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                _hotbar.transform.GetChild(0).GetComponent<InventoryItemController>().UseItem();
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                _hotbar.transform.GetChild(1).GetComponent<InventoryItemController>().UseItem();
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                _hotbar.transform.GetChild(2).GetComponent<InventoryItemController>().UseItem();
            else if (Input.GetKeyDown(KeyCode.Alpha4))
                _hotbar.transform.GetChild(3).GetComponent<InventoryItemController>().UseItem();
            else if (Input.GetKeyDown(KeyCode.Alpha5))
                _hotbar.transform.GetChild(4).GetComponent<InventoryItemController>().UseItem();
            else if (Input.GetKeyDown(KeyCode.Alpha6))
                _hotbar.transform.GetChild(5).GetComponent<InventoryItemController>().UseItem();
            else if (Input.GetKeyDown(KeyCode.Alpha7))
                _hotbar.transform.GetChild(6).GetComponent<InventoryItemController>().UseItem();
            else if (Input.GetKeyDown(KeyCode.Alpha8))
                _hotbar.transform.GetChild(7).GetComponent<InventoryItemController>().UseItem();
            else if (Input.GetKeyDown(KeyCode.Alpha9))
                _hotbar.transform.GetChild(8).GetComponent<InventoryItemController>().UseItem();
            else if (Input.GetKeyDown(KeyCode.Alpha0))
                _hotbar.transform.GetChild(9).GetComponent<InventoryItemController>().UseItem();
        }
        catch (System.Exception)
        {
            Debug.Log("No Item in Slot");
        }
       
    }

}
