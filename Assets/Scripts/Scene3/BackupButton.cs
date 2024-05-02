using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BackupButton : MonoBehaviour
{
    [SerializeField] private TMP_InputField textBlock;
    public int panelAnswer;
    public int panelSecret;
    public string panelName;
    [SerializeField] private GameObject panelUI;
    [SerializeField] private GameObject secretText;
    [SerializeField] private BreakerSwitch switch1;
    [SerializeField] private BreakerSwitch switch2;
    [SerializeField] private BreakerSwitch switch3;
    [SerializeField] private BreakerSwitch switch4;
    [SerializeField] private ImagePanel panel1;
    [SerializeField] private ImagePanel panel2;
    [SerializeField] private TextPanel panel3;
    [SerializeField] private TextPanel panel4;
    [SerializeField] private AudioSource buttonAudio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ButtonPressed(){
        if(panel1.isSecret && panel2.isSecret && panel3.isSecret && panel4.isSecret){
            secretText.SetActive(true);
        }
        if(textBlock.text == ""+panelAnswer){
            StartCoroutine(correctAns());
        } else{
            if(textBlock.text == ""+panelSecret){
                if(panelSecret == 2){
                    panel1.isSecret = true;
                } else if(panelSecret == 21){
                    panel2.isSecret = true;
                } else if(panelSecret == 18){
                    panel3.isSecret = true;
                } else if(panelSecret == 14){
                    panel4.isSecret = true;
                }
            }
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
