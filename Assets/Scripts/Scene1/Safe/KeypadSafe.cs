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
            base.OnMouseOver();
            if (!_canUnlock && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)) && IsWithinReach())
            {
                errorAudio.Play();
                CloseUI();
            }
        }

        public void AddNumb(Button button)
        {
            dial.Play();
            _entered += button.name;
            if (_entered.Length >= 5)
            {
                if (_entered == code)
                    OpenSafe();
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
                CloseUI();
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