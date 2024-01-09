using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SafeDoor2 : MonoBehaviour
{
    [SerializeField] private GameObject intText;
    private float[] _tempValues;
    [SerializeField] private float reach;
    [SerializeField] private GameObject padlockUI;
    private Animator _safeAnimator;
    private bool _isUnlocked;
    [SerializeField] private AudioSource dial;
    [SerializeField] private AudioSource unlockAudio;
    [SerializeField] private AudioSource errorAudio;
    public string code;
    private string _entered = "";
    private int _incorrectTrials;
    private bool _canUnlock = true;
    private static readonly int Unlock = Animator.StringToHash("unlock");

    // Start is called before the first frame update
    private void Start()
    {
        for (var i = 0; i < 5; i++)
        {
            code += Random.Range(0, 10);
        }
    }

    private void Awake()
    {
        _safeAnimator = GetComponentInChildren<Animator>();
    }

    private void OnMouseOver()
    {
        if (!padlockUI.activeSelf && !_isUnlocked)
            intText.SetActive(IsWithinReach());
        if (Input.GetKeyDown(KeyCode.E) && !_isUnlocked && IsWithinReach())
        {
            if (_canUnlock)
            {
                intText.SetActive(false);
                padlockUI.SetActive(!padlockUI.activeSelf);
                PauseMenu.IsPaused = true;
                Cursor.lockState = padlockUI.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
            }
            else
            {
                errorAudio.Play();
            }
        }
    }

    private void OnMouseExit()
    {
        intText.SetActive(false);
    }

    public void AddNumb(Button button)
    {
        Debug.Log("Hello!");
        dial.Play();
        _entered += button.name;
        if (_entered.Length >= 5)
        {
            if (_entered == code)
            {
                _safeAnimator.SetBool(Unlock, true);
                unlockAudio.Play();
                _isUnlocked = true;
            }
            else
            {
                _incorrectTrials++;
                if (_incorrectTrials >= 3)
                {
                    _canUnlock = false;
                    StartCoroutine(UntilReset());
                }
            }

            _entered = "";
            padlockUI.SetActive(false);
            PauseMenu.IsPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (padlockUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                padlockUI.SetActive(false);
                PauseMenu.IsPaused = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    private IEnumerator UntilReset()
    {
        yield return new WaitForSeconds(300f);
        _canUnlock = true;
        _incorrectTrials = 0;
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position) <= reach;
    }
}