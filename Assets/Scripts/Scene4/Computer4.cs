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
    // Start is called before the first frame update
    void Start()
    {
        greenBook = greenLetters[Random.Range(0,greenLetters.Length)];
        redBook = redLetters[Random.Range(0,redLetters.Length)];
        yellowBook = yellowLetters[Random.Range(0,yellowLetters.Length)];
        blueBook = blueLetters[Random.Range(0,blueLetters.Length)];
        code = GenerateRandomString();
        greenBook.text = code[0].ToString();
        redBook.text = code[1].ToString();
        yellowBook.text = code[2].ToString();
        blueBook.text = code[3].ToString();
    }
    string GenerateRandomString()
    {
        const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; // Define the alphabet
        string result = "";

        for (int i = 0; i < 4; i++)
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
}
