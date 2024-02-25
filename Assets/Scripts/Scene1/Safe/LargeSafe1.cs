using TMPro;
using UnityEngine;

namespace Scene1.Safe
{
    public class SafeDoor31 : Safe
    {
        public new void Update(){}

        private void OnMouseOver()
        {
            intText.GetComponent<TMP_Text>().text = "LOCKPICK NEEDED TO INTERACT";
            if (isUnlocked == false)
                intText.SetActive(IsWithinReach());
            else
                intText.SetActive(false);

            if (Input.GetMouseButtonDown(1) && IsWithinReach() && Player.Player.Instance.IsHolding("Lock Pick"))
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