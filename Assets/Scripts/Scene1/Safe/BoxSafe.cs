using TMPro;
using UnityEngine;

namespace Scene1.Safe
{
    public class BoxSafe : Safe
    {
        public new void Update(){}

        public void OnMouseExit(){}

        private void OnMouseOver()
        {
            intText.GetComponent<TMP_Text>().text = "SCREWDRIVER NEEDED TO INTERACT";
            if (_isUnlocked == false)
                intText.SetActive(IsWithinReach());
            else
                intText.SetActive(false);

            if (Input.GetMouseButtonDown(1) && IsWithinReach() && Player.Player.Instance.IsHolding("Screwdriver"))
            {
                _safeAnimator.SetBool(Unlock, true);
                if (!_isUnlocked) unlockAudio.Play();

                _isUnlocked = true;
                intText.SetActive(false);
            }
        }
    }
}