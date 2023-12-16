using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ascal : MonoBehaviour
{
    [SerializeField] private GameObject ascalUI;
    [SerializeField] private AudioSource ascalAudio;
    [SerializeField] private TMP_Text displayText;
    [SerializeField] private GameObject letterInput;
    public string[] words;
    [SerializeField] private string string1;
    [SerializeField] private string string2;
    [SerializeField] private string string3;
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
        string1 = words[Random.Range(0, words.Length)];
        string2 = words[Random.Range(0, words.Length)];
        string3 = words[Random.Range(0, words.Length)];
        
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
