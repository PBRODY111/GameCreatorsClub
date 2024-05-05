using Player.Inventory;
using TMPro;
using UI;
using UnityEngine;

namespace Scene1 {
    public class Ladder : InteractableObjectWithUI
    {
        [SerializeField] private GameObject ladderObj;
        [SerializeField] private Transform screws;
        private int[] rotations;
        private bool[] thingings;
        private bool hasPoles;
        private bool unlocked;

        public void Awake()
        {
            rotations = new[] { 45, 135, 90, 0, 90 };
            thingings = new[] { true, true, false, false, true };
        }

        public new void Update()
        {
            base.Update();
            if (ui.activeSelf && Screws())
            {
                ladderObj.SetActive(true);
                CloseUI();
                gameObject.SetActive(false);
            }
        }

        private void OnMouseEnter()
        {
            if (!unlocked)
                intText.GetComponent<TMP_Text>().text = (hasPoles ? "SCREWS" : "POLES") + " NEEDED TO INTERACT";
            else intText.GetComponent<TMP_Text>().text = "CLICK [E] TO INTERACT";
        }

        private new void OnMouseOver()
        {
            if (unlocked) base.OnMouseOver();
            else intText.SetActive(IsWithinReach());

            if (hasPoles && Input.GetMouseButtonDown(1) && IsWithinReach() && Player.Player.Instance.IsHolding("Screws") || Input.GetKeyDown(KeyCode.E) && unlocked)
            {
                Inventory.Instance.RemoveSelectedItem();
                unlocked = true;
                intText.GetComponent<TMP_Text>().text = "";
                OpenUI();
            }
            else if (!hasPoles && Input.GetMouseButtonDown(1) && IsWithinReach() && Player.Player.Instance.IsHolding("Poles"))
            {
                Inventory.Instance.RemoveSelectedItem();
                hasPoles = true;
                OnMouseEnter();
            }
            if(Input.GetKeyDown(KeyCode.E) && unlocked){
                intText.GetComponent<TMP_Text>().text = "";
                OpenUI();
            }
        }

        private bool Screws()
        {
            for (int i = 0; i < screws.childCount; i++)
            {
                int rotation = (int)screws.GetChild(i).rotation.eulerAngles.z;
                if ((rotation != rotations[i] && rotation != rotations[i] + 180) ||
                    (screws.GetChild(i).localScale.x.Equals(0.5f)) != thingings[i] ||
                    screws.GetChild(i).gameObject.activeSelf == false) return false;
            }
            return true;
        }
    }
}