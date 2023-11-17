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
    private typewriterUI typewriterUi;
    private string[] text =
    {
        "Dad, can you hear me?",
        "Where are you? Where have you been?",
        "Please come home.",
        "I'm trapped in this nightmare.",
        "There's something that's after me.",
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
        foreach (var line in text)
        {
            typewriterUi.setText(line);
            typewriterUi.Write();
            yield return new WaitForSeconds(typewriterUi.getTimeBetween() * line.Length + 1f);
        }
        audio.Stop();
        rose.SetActive(true);
        zagreus.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
