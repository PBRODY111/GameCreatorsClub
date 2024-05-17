using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Plank : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText3;
    [SerializeField] private AudioSource axe;
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
            intText3.GetComponent<TMP_Text>().text = "AXE NEEDED TO INTERACT";
            intText3.SetActive(true);
            if (Input.GetMouseButtonDown(1) && IsWithinReach()){
                if(Player.Player.Instance.IsHolding("Axe")){
                    axe.Play();
                    intText3.SetActive(false);
                    gameObject.SetActive(false);
                }
            }
        }
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
