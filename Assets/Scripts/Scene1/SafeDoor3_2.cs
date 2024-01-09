using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SafeDoor32 : MonoBehaviour
{
    [SerializeField] private GameObject intText;
    [SerializeField] private float reach;
    [SerializeField] private GameObject colorlockUI;
    private Animator _safeAnimator;
    private bool _isUnlocked;
    [SerializeField] private AudioSource dial;
    [SerializeField] private AudioSource unlockAudio;
    public string code;
    public Color selectColor;
    private int _colorIndex;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private SafeDoor31 safeDoor3;
    [SerializeField] private Ascal ascal;

    // Start is called before the first frame update
    private void Awake()
    {
        _safeAnimator = GetComponentInChildren<Animator>();
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

    private void OnMouseExit()
    {
        intText.SetActive(false);
    }

    public void ChangeColor(Button button)
    {
        button.GetComponent<Image>().color = ascal.colors[_colorIndex];
        dial.Play();
        if (inputField.text == code && code != "" && button.GetComponent<Image>().color == selectColor)
        {
            _safeAnimator.SetBool("unlock", true);
            if (!_isUnlocked)
            {
                unlockAudio.Play();
            }

            _isUnlocked = true;
            intText.SetActive(false);
            colorlockUI.SetActive(false);
            PauseMenu.IsPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        _colorIndex++;
        if (_colorIndex >= ascal.colors.Length)
        {
            _colorIndex = 0;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (colorlockUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                colorlockUI.SetActive(false);
                PauseMenu.IsPaused = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position) <= reach;
    }
}