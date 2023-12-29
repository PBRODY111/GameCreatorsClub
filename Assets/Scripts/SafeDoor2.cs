using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeDoor2 : MonoBehaviour
{
    [SerializeField] private GameObject intText;
    private float[] tempValues;
    [SerializeField] private float reach;
    [SerializeField] private GameObject padlockUI;
    private Animator safeAnimator;
    private bool isUnlocked = false;
    [SerializeField] private AudioSource dial;
    [SerializeField] private AudioSource unlockAudio;
    [SerializeField] private AudioSource errorAudio;
    public string code;
    private string entered = "";
    private int incorrectTrials = 0;
    private bool canUnlock = true;
    
    // Start is called before the first frame update
    void Start(){
        for(int i = 0; i<5; i++){
            code += Random.Range(0, 10);
        }
    }

    void Awake()
    {
        safeAnimator = GetComponentInChildren<Animator>();
    }

    void OnMouseOver()
    {
        if (!padlockUI.activeSelf && !isUnlocked)
            intText.SetActive(IsWithinReach());
        if (Input.GetKeyDown(KeyCode.E) && !isUnlocked && IsWithinReach())
        {
            if(canUnlock){
                intText.SetActive(false);
                padlockUI.SetActive(!padlockUI.activeSelf);
                PauseMenu.isPaused = true;
                Cursor.lockState = padlockUI.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
            } else{
                errorAudio.Play();
            }
        }


    }
    void OnMouseExit()
    {
        intText.SetActive(false);
    }

    public void AddNumb(Button button){
        Debug.Log("Hello!");
        dial.Play();
        entered += button.name;
        if(entered.Length >= 5){
            if(entered == code){
                safeAnimator.SetBool("unlock", true);
                unlockAudio.Play();
                isUnlocked = true;
            } else{
                incorrectTrials++;
                if(incorrectTrials >= 3){
                    canUnlock = false;
                    StartCoroutine(untilReset());
                }
            }
            entered = "";
            padlockUI.SetActive(false);
            PauseMenu.isPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (padlockUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                padlockUI.SetActive(false);
                PauseMenu.isPaused = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    IEnumerator untilReset(){
        yield return new WaitForSeconds(300f);
        canUnlock = true;
        incorrectTrials = 0;
    }

    bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position) <= reach;
    }
}
