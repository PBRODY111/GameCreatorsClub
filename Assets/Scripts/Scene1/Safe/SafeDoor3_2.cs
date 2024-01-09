using Scene1.Computer;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Scene1.Safe
{
    public class SafeDoor32 : MonoBehaviour
    {
        [SerializeField] private GameObject intText;
        [SerializeField] private float reach;
        [SerializeField] private GameObject colorlockUI;
        [SerializeField] private AudioSource dial;
        [SerializeField] private AudioSource unlockAudio;
        public string code;
        public Color selectColor;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private SafeDoor31 safeDoor3;
        [SerializeField] private Ascal ascal;
        private int _colorIndex;
        private bool _isUnlocked;
        private Animator _safeAnimator;
        private static readonly int Unlock = Animator.StringToHash("unlock");

        // Start is called before the first frame update
        private void Awake()
        {
            _safeAnimator = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (colorlockUI.activeSelf)
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    colorlockUI.SetActive(false);
                    PauseMenu.IsPaused = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
        }

        private void OnMouseExit()
        {
            intText.SetActive(false);
        }

        private void OnMouseOver()
        {
            if (safeDoor3.isUnlocked)
            {
                if (!colorlockUI.activeSelf && !_isUnlocked)
                    intText.SetActive(IsWithinReach());
                if (Input.GetKeyDown(KeyCode.E) && !_isUnlocked && IsWithinReach())
                {
                    intText.SetActive(false);
                    colorlockUI.SetActive(true);
                    PauseMenu.IsPaused = true;
                    Cursor.lockState = colorlockUI.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
                }
            }
        }

        public void ChangeColor(Button button)
        {
            button.GetComponent<Image>().color = ascal.colors[_colorIndex];
            dial.Play();
            if (inputField.text == code && code != "" && button.GetComponent<Image>().color == selectColor)
            {
                _safeAnimator.SetBool(Unlock, true);
                if (!_isUnlocked) unlockAudio.Play();

                _isUnlocked = true;
                intText.SetActive(false);
                colorlockUI.SetActive(false);
                PauseMenu.IsPaused = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

            _colorIndex++;
            if (_colorIndex >= ascal.colors.Length) _colorIndex = 0;
        }

        private bool IsWithinReach()
        {
            return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
        }
    }
}