using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

public class VentScrew : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private GameObject intText3;
    [SerializeField] private Vent vent;
    public UnityEvent rightClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            rightClick.Invoke ();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        intText3.GetComponent<TMP_Text>().text = "SCREWDRIVER NEEDED TO INTERACT";
        intText3.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        intText3.SetActive(false);
    }

    public void RotateButton(GameObject button){
        button.GetComponent<RectTransform>().Rotate(new Vector3( 0, 0, 6 ));
        button.GetComponent<AudioSource>().Play();
        if(button.transform.localRotation.eulerAngles.z >= 350){
            button.SetActive(false);
            intText3.SetActive(false);
            vent.unscrewed += 1;
        }
    }
}
