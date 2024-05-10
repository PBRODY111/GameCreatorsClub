using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Scene1;
using Scene1.Safe;
using TMPro;

public class KeypadDoor : Safe
{
    
    [SerializeField] private AudioSource dial;
    [SerializeField] private AudioSource errorAudio;
    [SerializeField] private AudioSource morseAudio;
    public string code;
    [SerializeField] private TMP_Text keypadText;
    private bool _canUnlock = true;
    [SerializeField] private string _entered = "";
    private int _incorrectTrials;
    private float[] _tempValues;
    [SerializeField] private Animator doorAnim;
    private static readonly int IsOpen = Animator.StringToHash("isOpen");
    private bool hasUnlocked = false;

    private void Start()
    {
        for (var i = 0; i < 7; i++) code += Random.Range(0, 10);
        code += "#";
        keypadText.text = code;
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
        if (_entered.Length >= 8)
        {
            if (_entered == code){
                doorAnim.SetBool(IsOpen, true);
                hasUnlocked = true;
                CloseUI();
                OpenSafe();
            }
            else
            {
                if(_entered == "2211814#"){
                    morseAudio.Play();
                }
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