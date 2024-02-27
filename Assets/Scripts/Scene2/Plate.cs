using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Player.Inventory;

public class Plate : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText3;
    [SerializeField] private GameObject beakerObj;
    // Start is called before the first frame update
    void Start()
    {
        beakerObj.SetActive(false);
    }

    private void OnMouseExit()
    {
        intText3.SetActive(false);
    }
    private void OnMouseOver()
    {
        intText3.GetComponent<TMP_Text>().text = "BEAKER NEEDED TO INTERACT";
        intText3.SetActive(IsWithinReach() && !beakerObj.activeSelf);

        if (Input.GetMouseButtonDown(1) && IsWithinReach() && Player.Player.Instance.IsHolding("Beaker"))
        {

            beakerObj.SetActive(true);
            Player.Player.Instance.hotbar.transform.GetChild(Player.Player.Instance.selectedslot).GetComponent<InventoryItemController>().RemoveItem();
            Player.Player.Instance.selectedslot = -1;
        }
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
