using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Computer4 : MonoBehaviour
{
    [SerializeField] private TextMeshPro [] greenLetters;
    [SerializeField] private TextMeshPro [] redLetters;
    [SerializeField] private TextMeshPro [] yellowLetters;
    [SerializeField] private TextMeshPro [] blueLetters;
    [SerializeField] private TextMeshPro greenBook;
    [SerializeField] private TextMeshPro redBook;
    [SerializeField] private TextMeshPro yellowBook;
    [SerializeField] private TextMeshPro blueBook;
    public string code;
    [SerializeField] private string pwd;
    [SerializeField] private TextMeshPro password;
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText;
    [SerializeField] private GameObject computerUI;
    [SerializeField] private GameObject loginPage;
    [SerializeField] private GameObject homePage;
    // Start is called before the first frame update

    private string _input;
    private readonly bool _isUnlocked = false;

    void Start()
    {
        greenBook = greenLetters[Random.Range(0,greenLetters.Length)];
        redBook = redLetters[Random.Range(0,redLetters.Length)];
        yellowBook = yellowLetters[Random.Range(0,yellowLetters.Length)];
        blueBook = blueLetters[Random.Range(0,blueLetters.Length)];
        code = GenerateRandomString(4);
        pwd = GenerateRandomString(5);
        password.text = pwd;
        greenBook.text = code[0].ToString();
        redBook.text = code[1].ToString();
        yellowBook.text = code[2].ToString();
        blueBook.text = code[3].ToString();
    }
    string GenerateRandomString(int lets)
    {
        const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; // Define the alphabet
        string result = "";

        for (int i = 0; i < lets; i++)
        {
            int randomIndex = Random.Range(0, alphabet.Length); // Generate a random index within the range of the alphabet
            result += alphabet[randomIndex]; // Append the character at the random index to the result string
        }

        return result;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (computerUI.activeSelf)
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //LogOut();
                ShutDown();
            }
    }

    private void OnMouseExit()
    {
        intText.SetActive(false);
    }

    private void OnMouseOver()
    {
        if (!computerUI.activeSelf && !_isUnlocked)
            intText.SetActive(IsWithinReach());
        if (Input.GetKeyDown(KeyCode.E) && !_isUnlocked && IsWithinReach())
        {
            intText.SetActive(false);
            computerUI.SetActive(true);
            Time.timeScale = 0f;
            Player.Player.Instance.UnlockCursor();
            Player.Player.Instance.DisableMovement();
        }
    }

    public void ShutDown()
    {
        Time.timeScale = 1f;
        Player.Player.Instance.LockCursor();
        Player.Player.Instance.EnableMovement();
        computerUI.SetActive(false);
    }
    public void LogIn(string s)
    {
        _input = s;
        if (_input == pwd || Player.Player.Instance.EpicModeEnabled())
        {
            Debug.Log("login");
            loginPage.SetActive(false);
            homePage.SetActive(true);
            //pwdField.Select();
            //pwdField.text = "";
        } else{
            Debug.Log(_input);
        }
    }

    public void LogOut()
    {
        loginPage.SetActive(true);
        homePage.SetActive(false);
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
