using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Player.Inventory;

public class MoveBookshelf : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText3;
    [SerializeField] private GameObject jack;
    private bool isIn = false;
    // Start is called before the first frame update
    private void OnMouseExit()
    {
        intText3.SetActive(false);
    }

    private void OnMouseOver()
    {
        if(IsWithinReach() && !isIn){

            intText3.GetComponent<TMP_Text>().text = "JACK NEEDED TO INTERACT";
            intText3.SetActive(true);

            if (Input.GetMouseButtonDown(1) && IsWithinReach() && Player.Player.Instance.IsHolding("Jack")){
                isIn = true;
                transform.position = new Vector3(transform.position.x, transform.position.y+0.2f, transform.position.z);
                jack.SetActive(true);
                Inventory.Instance.RemoveSelectedItem();
            }

        }
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
