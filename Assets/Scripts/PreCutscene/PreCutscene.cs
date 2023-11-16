using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreCutscene : MonoBehaviour
{   
    [SerializeField] private GameObject rose;
    private typewriterUI typewriterUi;
    private string[] text =
    {
        "DAD, CAN YOU HEAR ME?",
        "Where are you? Where have you been?",
        "I miss you.",
        "Please come home.",
        "I'm trapped in this nightmare.",
        "I'm so alone.",
        "I'm so scared.",
        "So scared..."
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
        rose.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
