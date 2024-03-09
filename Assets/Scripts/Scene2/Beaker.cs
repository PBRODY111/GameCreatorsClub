using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Player.Inventory;
using Scene2;

public class Beaker : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText;
    [SerializeField] private GameObject intText3;
    [SerializeField] private GameObject chemObj;
    [SerializeField] private GameObject hotplateUI;
    [SerializeField] private Hotplate hotPlate;
    [SerializeField] private Attack2 cer;
    [SerializeField] private bool correctChem = false;
    [SerializeField] private bool correctSize = false;
    [SerializeField] private string [] elements;
    private readonly bool _isUnlocked = false;
    // Start is called before the first frame update
    void Start()
    {
        chemObj.SetActive(false);
    }

    private void Update()
        {
            if (hotplateUI.activeSelf)
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    hotplateUI.SetActive(false);
                    Player.Player.Instance.LockCursor();
                    Player.Player.Instance.EnableMovement();
                }
        }

    private void OnMouseExit()
    {
        intText.SetActive(false);
        intText3.SetActive(false);
    }
    private void OnMouseOver()
    {
        if(correctSize){
            if (!hotplateUI.activeSelf && !_isUnlocked)
                intText.SetActive(IsWithinReach());
            if (Input.GetKeyDown(KeyCode.E) && !_isUnlocked && IsWithinReach())
            {
                intText.SetActive(false);
                hotplateUI.SetActive(true);
                Player.Player.Instance.UnlockCursor();
                Player.Player.Instance.DisableMovement();
            }
        } else{
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

                if (Input.GetMouseButtonDown(1) && IsWithinReach() && !correctSize && (Player.Player.Instance.IsHolding("L Stir")||Player.Player.Instance.IsHolding("S Stir")||Player.Player.Instance.IsHolding("M Stir")))
                {
                    if(!Player.Player.Instance.IsHolding(hotPlate.stirSize)){
                        cer.aggression = true;
                    }
                    correctSize = true;
                    intText3.SetActive(false);
                    Player.Player.Instance.hotbar.transform.GetChild(Player.Player.Instance.selectedslot).GetComponent<InventoryItemController>().RemoveItem();
                    Player.Player.Instance.selectedslot = -1;
                }
            }
        }
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
