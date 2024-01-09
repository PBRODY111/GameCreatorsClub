using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scene1
{
    public class LadderScrew : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private GameObject intText3;
        public UnityEvent rightClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
                rightClick.Invoke();
        }

        // Start is called before the first frame update
        public void OnPointerEnter(PointerEventData eventData)
        {
            intText3.GetComponent<TMP_Text>().text = "SCREWDRIVER NEEDED TO INTERACT";
            intText3.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            intText3.SetActive(false);
        }

        public void ChangeSize(Button button)
        {
            if (button.GetComponent<RectTransform>().rect.width == 50)
            {
                button.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(80, 80);
            }
            else
            {
                button.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(50, 50);
            }

            button.GetComponent<AudioSource>().Play();
        }

        public void RotateButton(Button button)
        {
            if (Player.Player.Instance.GetHeldItem().itemName == "Screwdriver")
            {
                button.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 45));
                button.GetComponent<AudioSource>().Play();
            }
        }

        // Update is called once per frame
        private void Update()
        {
        }
    }
}