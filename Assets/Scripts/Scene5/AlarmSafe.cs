using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AlarmSafe : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText;
    [SerializeField] private TextMeshPro alarmText;
    [SerializeField] private string leftText;
    [SerializeField] private string rightText;
    [SerializeField] private GameObject alarmUI;
    [SerializeField] private TMP_InputField lTextUI;
    [SerializeField] private TMP_InputField rTextUI;
    [SerializeField] private Animator doorAnim;
    [SerializeField] private AudioSource unlockAudio;
    private static readonly int IsOpen = Animator.StringToHash("isOpen");
    // Start is called before the first frame update
    void Start(){
        leftText = GenerateRandomNumberString();
        rightText = GenerateRandomNumberString();
        alarmText.text = leftText+":"+rightText;
    }
    private void Update()
    {
        if (alarmUI.activeSelf)
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                alarmUI.SetActive(false);
                PauseMenu.IsPaused = false;
                Player.Player.Instance.LockCursor();
            }
    }
    private void OnMouseExit()
    {
        intText.SetActive(false);
    }

    private void OnMouseOver()
    {
        if(IsWithinReach()){
            intText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && IsWithinReach())
            {
                alarmUI.SetActive(true);
                PauseMenu.IsPaused = true;
                Player.Player.Instance.UnlockCursor();
            }
        }
    }

    public void TextChanged(){
        if(lTextUI.text == leftText && rTextUI.text == rightText){
            doorAnim.SetBool(IsOpen, true);
            alarmUI.SetActive(false);
            PauseMenu.IsPaused = false;
            Player.Player.Instance.LockCursor();
            unlockAudio.Play();
        }
    }
    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }

    private string GenerateRandomNumberString()
    {
        int number1 = Random.Range(0, 6); // Generate a random number between 0 and 9
        int number2 = Random.Range(0, 10); // Generate another random number between 0 and 9

        string result = number1.ToString() + number2.ToString(); // Concatenate the two numbers into a string
        return result;
    }
}
