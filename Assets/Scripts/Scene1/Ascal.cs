using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ascal : MonoBehaviour
{
    [SerializeField] private GameObject ascalUI;
    [SerializeField] private AudioSource ascalAudio;
    [SerializeField] private AudioSource ascalEffects;
    public AudioClip[] effects;
    [SerializeField] private TMP_Text displayText;
    [SerializeField] private GameObject letterInput;
    public string[] words1;
    public string[] words2;
    public string[] words3;
    [SerializeField] private string string1;
    [SerializeField] private string string2;
    [SerializeField] private string string3;
    [SerializeField] private string currentString;
    public string safe3Pwd;
    [SerializeField] private Color[] colors;
    public Color safe3Color;
    private int letterIndex = 0;
    private typewriterUI typewriterUi;
    private string[] text =
    {
        "Hello there, you may call me Ascal.",
        "Can you help me remember?",
        "I can't remember anything after turning into this.",
        "I will give you a list of letters.",
        "You will have to enter the letters one by one in the same order.",
        "Are you ready?",
        "Here we go!",
        "in 3...",
        "2...",
        "1..."
    };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EnterAscal(){
        letterIndex = 0;
        string1 = words1[Random.Range(0, words1.Length)];
        string2 = words2[Random.Range(0, words2.Length)];
        string3 = words3[Random.Range(0, words3.Length)];
        safe3Pwd = ""+string1[Random.Range(0, string1.Length)]+string2[Random.Range(0, string2.Length)]+string3[Random.Range(0, string3.Length)];
        safe3Color = colors[Random.Range(0, colors.Length)];
        
        //scramble string 1
        char[] array = string1.ToCharArray();
        var rng = new System.Random();
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            var value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
        string1 = new string(array);

        // scramble string 2
        array = string2.ToCharArray();
        rng = new System.Random();
        n = array.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            var value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
        string2 = new string(array);
        
        // scramble string 3
        array = string3.ToCharArray();
        rng = new System.Random();
        n = array.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            var value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
        string3 = new string(array);

        ascalUI.SetActive(true);
        ascalAudio.Play();
        Debug.Log("Entered");
        Time.timeScale = 1f;
        typewriterUi = transform.GetComponent<typewriterUI>();
        StartCoroutine(ascalGame());
    }

    public void ExitAscal(){
        ascalUI.SetActive(false);
        ascalAudio.Stop();
    }

    public void getLetter(string s){
        s = s.ToLower();
        Debug.Log(""+currentString[letterIndex]);
        if(""+s == ""+currentString[letterIndex]){
            if(letterIndex == currentString.Length-1){
                ascalEffects.clip = effects[2];
                ascalEffects.Play();
            } else{
                ascalEffects.clip = effects[0];
                ascalEffects.Play();
            }
        } else{
            ascalEffects.clip = effects[1];
            ascalEffects.Play();
            StartCoroutine(ascalIncorrect());
        }
        if(letterIndex == currentString.Length-1){
            letterIndex = 0;
            if(currentString == string1){
                StartCoroutine(ascalGame1());
            } else if(currentString == string2){
                StartCoroutine(ascalGame2());
            } else{
                StartCoroutine(ascalWin());
            }
        } else{
            letterIndex++;
        }
        letterInput.GetComponent<TMP_InputField>().text = "";
    }

    IEnumerator ascalGame()
    {
        letterInput.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("ASCAL!");
        foreach (var line in text)
        {
            typewriterUi.setText(line);
            typewriterUi.Write();
            yield return new WaitForSeconds(typewriterUi.getTimeBetween() * line.Length + 1f);
        }
        yield return new WaitForSeconds(0.5f);
        for(int i=0; i<string1.Length; i++){
            displayText.text = ""+string1[i];
            yield return new WaitForSeconds(1.5f);
            displayText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
        displayText.text = "Now your turn!";
        letterInput.SetActive(true);
        currentString = string1;
    }

    IEnumerator ascalGame1(){
        Time.timeScale = 1f;
        Debug.Log("Level 1 done");
        letterInput.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        typewriterUi.setText("Correct! The letters were: ");
        typewriterUi.Write();
        yield return new WaitForSeconds(3f);
        for(int i=0; i<string1.Length; i++){
            if(string1[i] == safe3Pwd[0]){
                displayText.color = safe3Color;
            } else{
                displayText.color = Color.black;
            }
            displayText.text = ""+string1[i];
            yield return new WaitForSeconds(1.5f);
            displayText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
        displayText.color = Color.black;
        typewriterUi.setText("Round 2 starting...");
        typewriterUi.Write();
        yield return new WaitForSeconds(3f);
        for(int i=0; i<string2.Length; i++){
            displayText.text = ""+string2[i];
            yield return new WaitForSeconds(1.5f);
            displayText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
        displayText.text = "Now your turn!";
        letterInput.SetActive(true);
        currentString = string2;
    }

    IEnumerator ascalGame2(){
        Time.timeScale = 1f;
        Debug.Log("Level 2 done");
        letterInput.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        typewriterUi.setText("Correct! The letters were: ");
        typewriterUi.Write();
        yield return new WaitForSeconds(3f);
        for(int i=0; i<string2.Length; i++){
            if(string2[i] == safe3Pwd[1]){
                displayText.color = safe3Color;
            } else{
                displayText.color = Color.black;
            }
            displayText.text = ""+string2[i];
            yield return new WaitForSeconds(1.5f);
            displayText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
        displayText.color = Color.black;
        typewriterUi.setText("Round 3 starting...");
        typewriterUi.Write();
        yield return new WaitForSeconds(3f);
        for(int i=0; i<string3.Length; i++){
            displayText.text = ""+string3[i];
            yield return new WaitForSeconds(1.5f);
            displayText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
        displayText.text = "Now your turn!";
        letterInput.SetActive(true);
        currentString = string3;
    }

    IEnumerator ascalIncorrect(){
        Time.timeScale = 1f;
        letterInput.SetActive(false);
        typewriterUi.setText("Incorrect! Try again later...");
        yield return new WaitForSeconds(3f);
        ExitAscal();
    }

    IEnumerator ascalWin(){
        Time.timeScale = 1f;
        Debug.Log("Level 3 done");
        letterInput.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        typewriterUi.setText("Correct! The letters were: ");
        typewriterUi.Write();
        yield return new WaitForSeconds(3f);
        for(int i=0; i<string3.Length; i++){
            if(string3[i] == safe3Pwd[2]){
                displayText.color = safe3Color;
            } else{
                displayText.color = Color.black;
            }
            displayText.text = ""+string3[i];
            yield return new WaitForSeconds(1.5f);
            displayText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
        displayText.color = Color.black;
        ascalEffects.clip = effects[3];
        ascalEffects.Play();
        typewriterUi.setText("GAME WIN!!!");
        yield return new WaitForSeconds(3f);
        ExitAscal();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
