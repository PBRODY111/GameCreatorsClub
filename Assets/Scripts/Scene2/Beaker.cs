using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Player.Inventory;
using Scene2;

public class Beaker : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText3;
    [SerializeField] private GameObject chemObj;
    [SerializeField] private Hotplate hotPlate;
    [SerializeField] private Attack2 cer;
    [SerializeField] private bool correctChem = false;
    [SerializeField] private bool correctSize = false;
    // Start is called before the first frame update
    void Start()
    {
        chemObj.SetActive(false);
    }

    private void OnMouseExit()
    {
        intText3.SetActive(false);
    }
    private void OnMouseOver()
    {
        if(!correctChem){
            intText3.GetComponent<TMP_Text>().text = "JUG NEEDED TO INTERACT";
            intText3.SetActive(IsWithinReach() && !chemObj.activeSelf);

            if (Input.GetMouseButtonDown(1) && IsWithinReach() && !chemObj.activeSelf && (Player.Player.Instance.IsHolding("Y Jug")||Player.Player.Instance.IsHolding("G Jug")||Player.Player.Instance.IsHolding("W Jug")))
            {
                if(!Player.Player.Instance.IsHolding(hotPlate.chemicalName)){
                    cer.aggression = true;
                }
                correctChem = true;
                chemObj.SetActive(true);
                Player.Player.Instance.hotbar.transform.GetChild(Player.Player.Instance.selectedslot).GetComponent<InventoryItemController>().RemoveItem();
                Player.Player.Instance.selectedslot = -1;
            }
        } else{
            intText3.GetComponent<TMP_Text>().text = "STIR NEEDED TO INTERACT";
            intText3.SetActive(IsWithinReach() && !correctSize);

            if (Input.GetMouseButtonDown(1) && IsWithinReach() && !correctSize && (Player.Player.Instance.IsHolding("L Stir")||Player.Player.Instance.IsHolding("S Stir")))
            {
                if(!Player.Player.Instance.IsHolding(hotPlate.stirSize)){
                    cer.aggression = true;
                }
                correctSize = true;
                Player.Player.Instance.hotbar.transform.GetChild(Player.Player.Instance.selectedslot).GetComponent<InventoryItemController>().RemoveItem();
                Player.Player.Instance.selectedslot = -1;
            }
        }
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
