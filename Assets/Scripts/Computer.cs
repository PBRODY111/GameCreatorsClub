using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour
{
    [SerializeField] private GameObject intText;
    [SerializeField] private GameObject computerUI;
    [SerializeField] private GameObject loginPage;
    [SerializeField] private GameObject homePage;
    public InputField pwdField;
    [SerializeField] private float reach;
    [SerializeField] private string password;
    private bool isUnlocked = false;
    private string input;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseOver()
    {
        if (!computerUI.activeSelf && !isUnlocked)
            intText.SetActive(IsWithinReach());
        if (Input.GetKeyDown(KeyCode.E) && !isUnlocked && IsWithinReach())
        {
            intText.SetActive(false);
            computerUI.SetActive(true);
            Cursor.lockState = computerUI.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (computerUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                computerUI.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void ShutDown(){
        computerUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void LogIn(string s){
        input = s;
        if (input == password){
            loginPage.SetActive(false);
            homePage.SetActive(true);
        }
    }
    public void LogOut(){
        loginPage.SetActive(true);
        homePage.SetActive(false);
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
