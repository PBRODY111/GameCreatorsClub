using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UI;
using UnityEngine.UI;

public class TextPanel : MonoBehaviour
{
    [SerializeField] private TextMeshPro signText;
    [SerializeField] private TMP_Text signText2;
    [SerializeField] private bool signBool; // true is multiply
    [SerializeField] private GameObject intText;
    [SerializeField] private GameObject panelUI;
    [SerializeField] private GameObject imageSlot;
    [SerializeField] private GameObject textSlot;
    [SerializeField] private char symb;
    [SerializeField] private TextMeshPro[] symbols;
    [SerializeField] private int[] symNums = new int[3];
    [SerializeField] private int answer;
    [SerializeField] private float reach;
    [SerializeField] private PhoneAudio phoneAudio;
    [SerializeField] private BackupButton bckupBtn;
    [SerializeField] private int secret;
    public bool isSecret = false;
    //[SerializeField] private GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        int randomInt = Random.Range(0, 2);
        signBool = (randomInt == 0) ? false : true;
        if(signBool){
            signText.text = "x";
            answer = 1;
        } else{
            signText.text = "+";
            answer = 0;
        }
        for(int i = 0; i<symNums.Length; i++){
            symNums[i] = Random.Range(1,5);
            if(signBool){
                answer *= symNums[i];
            } else{
                answer += symNums[i];
            }
        }
        for(int i = 0; i<symbols.Length; i++){
            symbols[i].text = new string(symb, symNums[i]);
        }
    }

    private void OnMouseExit()
    {
        intText.SetActive(false);
    }

    private void OnMouseOver()
    {
        intText.SetActive(IsWithinReach() && phoneAudio.emergencyActive);
        if (Input.GetKeyDown(KeyCode.E) && IsWithinReach() && phoneAudio.emergencyActive)
        {
            panelUI.SetActive(true);
            imageSlot.SetActive(false);
            textSlot.SetActive(true);
            intText.SetActive(false);
            if(signBool){
                signText2.text = "x";
            } else{
                signText2.text = "+";
            }
            bckupBtn.panelAnswer = answer;
            bckupBtn.panelSecret = secret;
            bckupBtn.panelName = gameObject.name;
            textSlot.GetComponent<TMP_Text>().text = symb.ToString();
            Player.Player.Instance.UnlockCursor();
            Player.Player.Instance.DisableMovement();
        }
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            panelUI.SetActive(false);
            Player.Player.Instance.LockCursor();
            Player.Player.Instance.EnableMovement();
        }
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
