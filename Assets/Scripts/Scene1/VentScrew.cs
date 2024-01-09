using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class VentScrew : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText3;
    [SerializeField] private GameObject button;
    [SerializeField] private Vent vent;

    public void OnMouseOver()
    {
        intText3.GetComponent<TMP_Text>().text = "SCREWDRIVER NEEDED TO INTERACT";
        intText3.SetActive(IsWithinReach());

        if(Player.Instance.EpicModeEnabled()){
            RotateButton(button);
        }

        if(Input.GetMouseButtonDown(1) && IsWithinReach() && Player.Instance.GetHeldItem().itemName == "Screwdriver"){
            RotateButton(button);
        }
    }
    public void OnMouseExit()
    {
        intText3.SetActive(false);
    }

    public void RotateButton(GameObject button){
        var rotation = 6;
        button.transform.Rotate(new Vector3( 0, 0, 6 ));
        button.GetComponent<AudioSource>().Play();
        if(button.transform.rotation.eulerAngles.y >= 350){
            button.SetActive(false);
            intText3.SetActive(false);
            vent.unscrewed += 1;
            if(vent.unscrewed >= 3){
                vent.growlAudio.Play();
            }
            if(vent.unscrewed >= 4){
                vent.doorSlam.Play();
                vent.footsteps.Play();
            }
        }
    }

    bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position) <= reach;
    }
}
