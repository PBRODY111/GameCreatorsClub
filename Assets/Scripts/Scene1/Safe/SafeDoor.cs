using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Scene1.Safe
{
    public class SafeDoor : MonoBehaviour
    {
        [SerializeField] private GameObject intText;
        [SerializeField] private float[] dialValues;
        [SerializeField] private float reach;
        [SerializeField] private GameObject dialUI;
        [SerializeField] private AudioSource unlockAudio;
        private int _currentDialValueIndex;
        private bool _isUnlocked;
        private Animator _safeAnimator;
        private Slider _slider;
        private float _sliderPreviousValue;
        private float[] _tempValues;
        private static readonly int Unlock = Animator.StringToHash("unlock");

        private void Awake()
        {
            _slider = dialUI.GetComponentInChildren<Slider>();
            _safeAnimator = GetComponentInChildren<Animator>();
            _tempValues = new float[dialValues.Length];
            _slider.onValueChanged.AddListener(delegate
            {
                if (IsWithinValue(_sliderPreviousValue, _slider.value, 1f)) GetComponent<AudioSource>().Play();
                _sliderPreviousValue = _slider.value;
            });
        }

        private void Update()
        {
            if (dialUI.activeSelf)
            {
                HandleEscapeKey();
                HandleMouseUp();
            }
        }

        private void HandleEscapeKey()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                dialUI.SetActive(false);
                PauseMenu.IsPaused = false;
                Player.Player.Instance.LockCursor();
                Player.Player.Instance.EnableMovement();
            }
        }

        private void HandleMouseUp()
        {
            if (!Input.GetKeyUp(KeyCode.Mouse0)) return;
            
            if (IsWithinValue(dialValues[_currentDialValueIndex], _slider.value, 2f))
            {
                _currentDialValueIndex++;
                
                if (_currentDialValueIndex >= dialValues.Length)
                {
                    _safeAnimator.SetBool(Unlock, true);
                    unlockAudio.Play();
                    dialUI.SetActive(false);
                    PauseMenu.IsPaused = false;
                    Player.Player.Instance.LockCursor();
                    Player.Player.Instance.EnableMovement();
                    _isUnlocked = true;
                }
            }
            else _currentDialValueIndex = 0;

            _slider.value = 0;
        }

        private void OnMouseExit()
        {
            if (!dialUI.activeSelf)
                intText.SetActive(false);
        }

        private void OnMouseOver()
        {
            if (!dialUI.activeSelf && !_isUnlocked)
                intText.SetActive(IsWithinReach());
            if (Input.GetKeyDown(KeyCode.E) && !_isUnlocked && IsWithinReach())
            {
                intText.SetActive(false);
                dialUI.SetActive(true);
                PauseMenu.IsPaused = true;
                Player.Player.Instance.DisableMovement();
                Player.Player.Instance.UnlockCursor();
            }
        }

        private bool IsWithinReach()
        {
            return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
        }

        private bool IsWithinValue(float value, float actual, float deviation)
        {
            return actual >= value - deviation && actual <= value + deviation;
        }
    }
}