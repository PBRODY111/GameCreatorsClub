using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Player.Inventory;

public class Keyhole : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText3;
    [SerializeField] private string intText3Text;
    [SerializeField] private GameObject keyObj;
    [SerializeField] private string keyName;
    [SerializeField] private SafeDoor5 lid;
    public bool hasKey = false;
    
    private void OnMouseExit()
    {
        intText3.SetActive(false);
    }
    private void OnMouseOver()
    {
        intText3.GetComponent<TMP_Text>().text = intText3Text;
        intText3.SetActive(IsWithinReach() && !hasKey);

        if (Input.GetMouseButtonDown(1) && IsWithinReach() && Player.Player.Instance.IsHolding(keyName) && !hasKey)
        {

            keyObj.SetActive(true);
            hasKey = true;
            Player.Player.Instance.hotbar.transform.GetChild(Player.Player.Instance.selectedslot).GetComponent<InventoryItemController>().RemoveItem();
            Player.Player.Instance.selectedslot = -1;
            lid.CheckUnlock();
        }
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
