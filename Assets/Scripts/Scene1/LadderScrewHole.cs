using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

public class LadderScrewHole : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private GameObject intText3;
    [SerializeField] private GameObject button2;
    public UnityEvent rightClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            rightClick.Invoke();
    }

    // Start is called before the first frame update
    public void OnPointerEnter(PointerEventData eventData)
    {
        intText3.GetComponent<TMP_Text>().text = "SCREWS NEEDED TO INTERACT";
        intText3.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        intText3.SetActive(false);
    }

    public void ShowScrew(GameObject button)
    {
        if (Player.Instance.GetHeldItem().itemName == "Screws")
        {
            button2.SetActive(true);
            button.SetActive(false);
        }
    }
}