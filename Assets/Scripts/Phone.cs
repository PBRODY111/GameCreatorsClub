using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour
{
    [SerializeField] private GameObject phoneUI;
    [SerializeField] private GameObject intText;
    [SerializeField] private float reach;
    [SerializeField] private string numb1;
    [SerializeField] private string numb2;
    [SerializeField] private string numb3;
    [SerializeField] private string numb4;
    [SerializeField] private string numb5;
    [SerializeField] private AudioSource dial;
    [SerializeField] private AudioSource call;
    [SerializeField] private AudioSource numb1Audio;
    [SerializeField] private AudioSource numb2Audio;
    [SerializeField] private AudioSource numb5Audio;
    [SerializeField] private AudioSource incomplete;
    private string entered = "";
    private bool isUnlocked = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
            /*
            if(entered == numb1){
                numb1Audio.Play();
            } else if(entered == numb2){
                numb2Audio.Play();
            } else if(entered == numb5){
                numb5Audio.Play();
            }
            phoneUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            entered = "";
            */
        }
    }
    IEnumerator waitDial()
    {
        phoneUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(8.0f);
        if(entered == numb1){
            numb1Audio.Play();
        } else if(entered == numb2){
            numb2Audio.Play();
        } else if(entered == numb5){
            numb5Audio.Play();
        } else{
            incomplete.Play();
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
