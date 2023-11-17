using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreCutscene : MonoBehaviour
{   
    [SerializeField] private GameObject rose;
    [SerializeField] private GameObject zagreus;
    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioSource audio2;
    private typewriterUI typewriterUi;
    private string[] text =
    {
        "Dad, can you hear me?",
        "Where are you?",
        "Please come home.",
        "I'm trapped in this nightmare.",
        "There's something after me.",
        "I don't know what it is.",
        "I'm so alone.",
        "I'm so scared.",
        "Please, help me.",
        "Help me.",
        "Please..."
    };
    void Start()
    {
        typewriterUi = transform.GetComponent<typewriterUI>();
        StartCoroutine(cutScene());
    }

    IEnumerator cutScene()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (var line in text)
        {
            typewriterUi.setText(line);
            typewriterUi.Write();
            yield return new WaitForSeconds(typewriterUi.getTimeBetween() * line.Length + 1f);
        }
        audio.Stop();
        audio2.Play();
        rose.SetActive(true);
        zagreus.SetActive(false);
        typewriterUi.setText("There is no escape.");
        typewriterUi.Write();
        yield return new WaitForSeconds(10f);
        rose.SetActive(false);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
