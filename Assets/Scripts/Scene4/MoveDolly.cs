using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Player.Inventory;

public class MoveDolly : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText3;
    [SerializeField] private GameObject dolly;
    // Start is called before the first frame update
    private void OnMouseExit()
    {
        intText3.SetActive(false);
    }

    private void OnMouseOver()
    {
        if(IsWithinReach()){

            intText3.GetComponent<TMP_Text>().text = "DOLLY NEEDED TO INTERACT";
            intText3.SetActive(true);

            if (Input.GetMouseButtonDown(1) && IsWithinReach() && Player.Player.Instance.IsHolding("Dolly")){
                transform.position = new Vector3(transform.position.x, transform.position.y+0.2f, transform.position.z);
                dolly.SetActive(true);
                Player.Player.Instance.hotbar.transform.GetChild(Player.Player.Instance.selectedslot).GetComponent<InventoryItemController>().RemoveItem();
                Player.Player.Instance.selectedslot = -1;
                gameObject.SetActive(false);
            }

        }
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
