using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Scene1.Safe
{
    public class SafeDoor2 : MonoBehaviour
    {
        private static readonly int Unlock = Animator.StringToHash("unlock");
        [SerializeField] private GameObject intText;
        [SerializeField] private float reach;
        [SerializeField] private GameObject padlockUI;
        [SerializeField] private AudioSource dial;
        [SerializeField] private AudioSource unlockAudio;
        [SerializeField] private AudioSource errorAudio;
        public string code;
        private bool _canUnlock = true;
        private string _entered = "";
        private int _incorrectTrials;
        private bool _isUnlocked;
        private Animator _safeAnimator;
        private float[] _tempValues;

        private void Awake()
        {
            _safeAnimator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            for (var i = 0; i < 5; i++) code += Random.Range(0, 10);
        }

        private void Update()
        {
            if (padlockUI.activeSelf)
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    padlockUI.SetActive(false);
                    PauseMenu.IsPaused = false;
                    Player.Player.Instance.LockCursor();
                    Player.Player.Instance.EnableMovement();
                }
        }

        private void OnMouseExit()
        {
            intText.SetActive(false);
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
                    Player.Player.Instance.UnlockCursor();
                    Player.Player.Instance.DisableMovement();
                }
                else
                {
                    errorAudio.Play();
                }
            }
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
                Player.Player.Instance.LockCursor();
                Player.Player.Instance.EnableMovement();
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
            return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
        }
    }
}