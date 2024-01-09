using UnityEngine;
using UnityEngine.UI;

public class SafeDoor : MonoBehaviour
{
    [SerializeField] private GameObject intText;
    [SerializeField] private float[] dialValues;
    private float[] _tempValues;
    [SerializeField] private float reach;
    [SerializeField] private GameObject dialUI;
    private Animator _safeAnimator;
    private Slider _slider;
    private float _sliderPreviousValue;
    private int _currentDialValueIndex;
    private bool _isUnlocked;
    [SerializeField] private AudioSource unlockAudio;

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

    private void OnMouseOver()
    {
        if (!dialUI.activeSelf && !_isUnlocked)
            intText.SetActive(IsWithinReach());
        if (Input.GetKeyDown(KeyCode.E) && !_isUnlocked && IsWithinReach())
        {
            intText.SetActive(false);
            dialUI.SetActive(true);
            PauseMenu.IsPaused = true;
            Cursor.lockState = dialUI.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    private void OnMouseExit()
    {
        if (!dialUI.activeSelf)
            intText.SetActive(false);
    }

    private void Update()
    {
        if (dialUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                dialUI.SetActive(false);
                PauseMenu.IsPaused = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (IsWithinValue(dialValues[_currentDialValueIndex], _slider.value, 2f))
                {
                    _currentDialValueIndex++;
                    if (_currentDialValueIndex >= dialValues.Length)
                    {
                        _safeAnimator.SetBool("unlock", true);
                        unlockAudio.Play();
                        dialUI.SetActive(false);
                        PauseMenu.IsPaused = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        _isUnlocked = true;
                    }
                }
                else
                {
                    _currentDialValueIndex = 0;
                }

                _slider.value = 0;
            }
        }
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position) <= reach;
    }

    private bool IsWithinValue(float value, float actual, float deviation)
    {
        return actual >= value - deviation && actual <= value + deviation;
    }
}