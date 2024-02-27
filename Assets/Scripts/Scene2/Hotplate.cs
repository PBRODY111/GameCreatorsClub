using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Player.Inventory;

public class Hotplate : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText3;
    [SerializeField] private GameObject plateObj;
    public string chemicalName;
    [SerializeField] private string [] chemicalNames;
    public string stirSize;
    [SerializeField] private string [] stirSizes;
    // Start is called before the first frame update
    void Start()
    {
        plateObj.SetActive(false);
        chemicalName = chemicalNames[Random.Range(0,3)];
        stirSize = stirSizes[Random.Range(0,2)];
    }

    private void OnMouseExit()
    {
        intText3.SetActive(false);
    }
    private void OnMouseOver()
    {
        intText3.GetComponent<TMP_Text>().text = "HOTPLATE NEEDED TO INTERACT";
        intText3.SetActive(IsWithinReach() && !plateObj.activeSelf);

        if (Input.GetMouseButtonDown(1) && IsWithinReach() && Player.Player.Instance.IsHolding("Hotplate"))
        {

            plateObj.SetActive(true);
            Player.Player.Instance.hotbar.transform.GetChild(Player.Player.Instance.selectedslot).GetComponent<InventoryItemController>().RemoveItem();
            Player.Player.Instance.selectedslot = -1;
        }
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
