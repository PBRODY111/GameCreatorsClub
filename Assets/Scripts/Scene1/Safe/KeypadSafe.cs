using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Scene1.Safe
{
    public class KeypadSafe : Safe
    {
        
        [SerializeField] private AudioSource dial;
        [SerializeField] private AudioSource errorAudio;
        public string code;
        private bool _canUnlock = true;
        private string _entered = "";
        private int _incorrectTrials;
        private float[] _tempValues;

        private void Start()
        {
            for (var i = 0; i < 5; i++) code += Random.Range(0, 10);
        }

        private new void OnMouseOver()
        {
            if (!ui.activeSelf && !_isUnlocked)
                intText.SetActive(IsWithinReach());
            if (Input.GetKeyDown(KeyCode.E) && !_isUnlocked && IsWithinReach())
            {
                if (_canUnlock)
                {
                    intText.SetActive(false);
                    ui.SetActive(true);
                    PauseMenu.IsPaused = true;
                    Player.Player.Instance.DisableMovement();
                    Player.Player.Instance.UnlockCursor(); 
                }
                else
                    errorAudio.Play();
                
            }
        }

        public void AddNumb(Button button)
        {
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
                ui.SetActive(false);
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
    }
}