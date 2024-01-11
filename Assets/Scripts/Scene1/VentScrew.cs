using TMPro;
using UnityEngine;

namespace Scene1
{
    public class VentScrew : MonoBehaviour
    {
        [SerializeField] private float reach;
        [SerializeField] private GameObject intText3;
        [SerializeField] private GameObject button;
        [SerializeField] private Vent vent;

        public void OnMouseExit()
        {
            intText3.SetActive(false);
        }

        public void OnMouseOver()
        {
            intText3.GetComponent<TMP_Text>().text = "SCREWDRIVER NEEDED TO INTERACT";
            intText3.SetActive(IsWithinReach());

            var pressed = Input.GetMouseButtonDown(1);

            if (pressed)
            {
                if (Player.Player.Instance.EpicModeEnabled())
                    RemoveButton();

                else if (Player.Player.Instance.IsHolding("Screwdriver"))
                    RotateButton();
            }
        }

        public void RotateButton()
        {
            button.transform.Rotate(new Vector3(0, 0, 17));
            button.GetComponent<AudioSource>().Play();
            if (button.transform.rotation.eulerAngles.y >= 350)
            {
                RemoveButton();
            }
        }
        
        private void RemoveButton()
        {
            button.SetActive(false);
            intText3.SetActive(false);
            vent.unscrewed += 1;
            if (vent.unscrewed >= 3) vent.growlAudio.Play();

            if (vent.unscrewed >= 4)
            {
                vent.doorSlam.Play();
                vent.footsteps.Play();
            }
        }

        private bool IsWithinReach()
        {
            return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
        }
    }
}