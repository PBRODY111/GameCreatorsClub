using Scene1.Computer;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Scene1.Safe
{
    public class SafeDoor32 : Safe
    {
        [SerializeField] private AudioSource dial;
        public string code;
        public Color selectColor;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private SafeDoor31 safeDoor3;
        [SerializeField] private Ascal ascal;
        private int _colorIndex;
        public void ChangeColor(Button button)
        {
            button.GetComponent<Image>().color = ascal.colors[_colorIndex];
            dial.Play();
            if (inputField.text == code && code != "" && button.GetComponent<Image>().color == selectColor)
            {
                _safeAnimator.SetBool(Unlock, true);
                if (!isUnlocked) unlockAudio.Play();

                isUnlocked = true;
                CloseUI();
            }

            _colorIndex++;
            if (_colorIndex >= ascal.colors.Length) _colorIndex = 0;
        }
    }
}