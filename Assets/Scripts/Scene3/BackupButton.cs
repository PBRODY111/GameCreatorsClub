using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BackupButton : MonoBehaviour
{
    [SerializeField] private TMP_InputField textBlock;
    public int panelAnswer;
    public string panelName;
    [SerializeField] private GameObject panelUI;
    [SerializeField] private BreakerSwitch switch1;
    [SerializeField] private BreakerSwitch switch2;
    [SerializeField] private BreakerSwitch switch3;
    [SerializeField] private BreakerSwitch switch4;
    [SerializeField] private AudioSource buttonAudio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ButtonPressed(){
        if(textBlock.text == ""+panelAnswer){
            StartCoroutine(correctAns());
        } else{
            textBlock.text = "";
            panelUI.SetActive(false);
            Player.Player.Instance.LockCursor();
            Player.Player.Instance.EnableMovement();
        }
    }

    IEnumerator correctAns(){
        buttonAudio.Play();
        if(panelName == "backuppanel"){
            textBlock.text = "S1: "+switch1.switchDirection;
        } else if(panelName == "backuppanel (1)"){
            textBlock.text = "S2: "+switch2.switchDirection;
        } else if(panelName == "backuppanel (2)"){
            textBlock.text = "S3: "+switch3.switchDirection;
        } else if(panelName == "backuppanel (3)"){
            textBlock.text = "S4: "+switch4.switchDirection;
        }
        yield return new WaitForSeconds(1.5f);
        textBlock.text = "";
        panelUI.SetActive(false);
        Player.Player.Instance.LockCursor();
        Player.Player.Instance.EnableMovement();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
