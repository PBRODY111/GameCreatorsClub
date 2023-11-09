using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Canvas _hotbar;
    [SerializeField] private Canvas _inventory;
    public KeyCode InventoryKey = KeyCode.E;

    private void Start()
    {
        _inventory.gameObject.SetActive(false);
        _hotbar.gameObject.SetActive(true);
    }
    void Update()
    {

        if (Input.GetKeyDown(InventoryKey))
        {
            if (_hotbar.gameObject.activeSelf)
            {
                _hotbar.gameObject.SetActive(false);
                _inventory.gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                _hotbar.gameObject.SetActive(true);
                _inventory.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
