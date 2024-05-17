using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Inventory;
using TMPro;

public class VendingMachine : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText3;
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject target;
    [SerializeField] private Transform filePath;
    [SerializeField] private AudioSource payment;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseExit()
    {
        intText3.SetActive(false);
    }

    private void OnMouseOver()
    {
        if(IsWithinReach()){
            intText3.GetComponent<TMP_Text>().text = "COIN NEEDED TO INTERACT";
            intText3.SetActive(true);

            if (Input.GetMouseButtonDown(1) && IsWithinReach()){
                if(Player.Player.Instance.IsHolding("Coin")){
                    Instantiate(prefab, target.transform.position, new Quaternion(), filePath);
                    payment.Play();
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
