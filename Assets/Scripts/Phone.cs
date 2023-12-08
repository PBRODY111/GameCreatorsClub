using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour
{
    [SerializeField] private GameObject phoneUI;
    [SerializeField] private GameObject intText;
    [SerializeField] private float reach;
    public string numb1;
    public string numb2;
    public string numb3;
    public string numb4;
    [SerializeField] private string numb5;
    [SerializeField] private AudioSource dial;
    [SerializeField] private AudioSource call;
    [SerializeField] private AudioSource numbAudio;
    public AudioClip[] numbers;
    private string entered = "";
    private bool isUnlocked = false;
    
    // Start is called before the first frame update
    void Start()
    {
        /*
        HashSet<string> set = new HashSet<string>() {
            numb1, numb2, numb3, numb4, "2211814"
        };  
        */
        //while(set.Count != 4){
        //}
        for(int i = 0; i<7; i++){
            numb1 += Random.Range(0, 10);
        }
        while(numb2 == "" || numb2 == numb1 || numb2 == numb5){
            numb2 = "";
            for(int i = 0; i<7; i++){
                numb2 += Random.Range(0, 10);
            }
        }
        while(numb3 == "" || numb3 == numb2 || numb2 == numb5){
            numb3 = "";
            for(int i = 0; i<7; i++){
                numb3 += Random.Range(0, 10);
            }
        }
        while(numb4 == "" || numb4 == numb3 || numb2 == numb5){
            numb4 = "";
            for(int i = 0; i<7; i++){
                numb4 += Random.Range(0, 10);
            }
        }
    }

    // Update is called once per frame
    void OnMouseOver()
    {
        if (!phoneUI.activeSelf && !isUnlocked)
            intText.SetActive(IsWithinReach());
        if (Input.GetKeyDown(KeyCode.E) && !isUnlocked && IsWithinReach())
        {
            intText.SetActive(false);
            phoneUI.SetActive(true);
            Cursor.lockState = phoneUI.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
        }


    }
    void OnMouseExit()
    {
        intText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (phoneUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                phoneUI.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                entered = "";
            }
        }
    }

    public void AddNumb(Button button){
        dial.Play();
        entered += button.name;
        if(entered.Length >= 7){
            call.Play();
            StartCoroutine(waitDial());
        }
    }
    IEnumerator waitDial()
    {
        phoneUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(8.0f);
        if(entered == numb1){
            numbAudio.clip = numbers[1];
            numbAudio.Play();
        } else if(entered == numb2){
            numbAudio.clip = numbers[2];
            numbAudio.Play();
        } else if(entered == numb5){
            numbAudio.clip = numbers[3];
            numbAudio.Play();
        } else{
            numbAudio.clip = numbers[0];
            numbAudio.Play();
        }
        entered = "";
    }

    bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position) <= reach;
    }

    bool IsWithinValue(float value, float actual,float deviation)
    {
        return actual >= value - deviation && actual <= value + deviation;
    }
}
