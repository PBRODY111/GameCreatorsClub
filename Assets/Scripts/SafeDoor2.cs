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
    [SerializeField] private string code;
    private string entered = "";
    
    // Start is called before the first frame update
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
            intText.SetActive(false);
            padlockUI.SetActive(!padlockUI.activeSelf);
            Cursor.lockState = padlockUI.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
        }


    }

    public void AddNumb(Button button){
        dial.Play();
        entered += button.name;
        if(entered.Length >= 5){
            if(entered == code){
                safeAnimator.SetBool("unlock", true);
                unlockAudio.Play();
                isUnlocked = true;
            }
            entered = "";
            padlockUI.SetActive(false);
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
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position) <= reach;
    }
}
