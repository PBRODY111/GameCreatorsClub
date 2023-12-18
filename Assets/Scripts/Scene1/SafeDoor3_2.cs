using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SafeDoor3_2 : MonoBehaviour
{
    [SerializeField] private GameObject intText;
    [SerializeField] private float reach;
    [SerializeField] private GameObject colorlockUI;
    private Animator safeAnimator;
    private bool isUnlocked = false;
    [SerializeField] private AudioSource dial;
    [SerializeField] private AudioSource unlockAudio;
    public string code;
    public Color selectColor;
    private int colorIndex = 0;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] private SafeDoor3_1 safeDoor3;
    [SerializeField] private Ascal ascal;
    
    // Start is called before the first frame update
    void Awake()
    {
        safeAnimator = GetComponentInChildren<Animator>();
    }

    void OnMouseOver()
    {
        if(safeDoor3.isUnlocked){
            if (!colorlockUI.activeSelf && !isUnlocked)
                intText.SetActive(IsWithinReach());
            if (Input.GetKeyDown(KeyCode.E) && !isUnlocked && IsWithinReach())
            {
                intText.SetActive(false);
                colorlockUI.SetActive(true);
                PauseMenu.isPaused = true;
                Cursor.lockState = colorlockUI.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
            }
        }
    }
    void OnMouseExit()
    {
        intText.SetActive(false);
    }

    public void changeColor(Button button){
        button.GetComponent<Image>().color = ascal.colors[colorIndex];
        dial.Play();
        if(inputField.text == code && code != "" && button.GetComponent<Image>().color == selectColor){
            safeAnimator.SetBool("unlock", true);
            if(!isUnlocked){
                unlockAudio.Play();
            }
            isUnlocked = true;
            intText.SetActive(false);
            colorlockUI.SetActive(false);
        }
        colorIndex++;
        if(colorIndex >= ascal.colors.Length){
            colorIndex = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (colorlockUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                colorlockUI.SetActive(false);
                PauseMenu.isPaused = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position) <= reach;
    }
}
