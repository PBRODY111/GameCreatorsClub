using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Scene1
{
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
            if (Player.Player.Instance.IsHolding("Screws"))
            {
                button2.SetActive(true);
                button.SetActive(false);
            }
        }
    }
}