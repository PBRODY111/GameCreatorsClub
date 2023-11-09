using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    public KeyCode InventoryKey = KeyCode.E;

    void Update()
    {
        if (Input.GetKeyDown(InventoryKey))
        {
            _canvas.gameObject.SetActive(!_canvas.gameObject.activeSelf);
            Cursor.lockState = _canvas.gameObject.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = _canvas.gameObject.activeSelf;
            if(_canvas.gameObject.activeSelf) 
                Inventory.Instance.ListItems();
            else 
                Inventory.Instance.CleanList();
        }
    }
}
