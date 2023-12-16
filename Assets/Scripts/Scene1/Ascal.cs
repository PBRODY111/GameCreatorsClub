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
    private int letterIndex = 0;
    private typewriterUI typewriterUi;
    private string[] text =
    {
        /*"Hello there, you may call me Ascal.",
        "Can you help me remember?",
        "I can't remember anything after turning into this.",
        "I will give you a list of letters.",
        "You will have to enter the letters one by one in the same order.",
        "Are you ready?",
        "Here we go!",
        */
        "in 3...",
        "2...",
        "1..."
    };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EnterAscal(){
        string1 = words1[Random.Range(0, words1.Length)];
        string2 = words2[Random.Range(0, words2.Length)];
        string3 = words3[Random.Range(0, words3.Length)];
        
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
        }
        if(letterIndex == currentString.Length-1){
            letterIndex = 0;
            if(currentString == string1){
                StartCoroutine(ascalGame1());
            }
        } else{
            letterIndex++;
        }
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
        letterInput.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        currentString = "That is correct! The letters were was: ";
        typewriterUi.setText(currentString);
        typewriterUi.Write();
        yield return new WaitForSeconds(typewriterUi.getTimeBetween() * currentString.Length + 1f);
        typewriterUi.setText(string1);
        typewriterUi.Write();
        yield return new WaitForSeconds(typewriterUi.getTimeBetween() * string1.Length + 1f);
        currentString = "Now here we go again!";
        typewriterUi.setText(currentString);
        typewriterUi.Write();
        yield return new WaitForSeconds(typewriterUi.getTimeBetween() * currentString.Length + 1f);
        currentString = "Round 2 starting...";
        typewriterUi.setText(currentString);
        typewriterUi.Write();
        yield return new WaitForSeconds(typewriterUi.getTimeBetween() * currentString.Length + 1f);
        yield return new WaitForSeconds(1.5f);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
