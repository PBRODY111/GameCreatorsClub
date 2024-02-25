using TMPro;
using UnityEngine;

namespace Scene1.Safe
{
    public class BoxSafe : Safe
    {
        public new void Update(){}
        private new void OnMouseOver()
        {
            intText.GetComponent<TMP_Text>().text = "SCREWDRIVER NEEDED TO INTERACT";
            if (isUnlocked == false)
                intText.SetActive(IsWithinReach());
            else
                intText.SetActive(false);

            if (Input.GetMouseButtonDown(1) && IsWithinReach() && Player.Player.Instance.IsHolding("Screwdriver"))
                OpenSafe();
        }
        
        private new void OpenSafe()
        {
            _safeAnimator.SetBool(Unlock, true);
            unlockAudio.Play();
            isUnlocked = true;
        }
    }
}