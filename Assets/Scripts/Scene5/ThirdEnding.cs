using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Steamworks;

public class ThirdEnding : MonoBehaviour
{
    [SerializeField] private Animator escapeAnim;
    [SerializeField] private GameObject escapeText;
    [SerializeField] private GameObject escapeUI;
    private AudioSource[] _allAudioSources;
    private static readonly int IsEscape = Animator.StringToHash("isEscape");
    private bool hasEntered = false;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Zagreus" && !hasEntered)
        {
            hasEntered = true;
            StartCoroutine(EscapeFunc());
        }
    }

    public IEnumerator EscapeFunc()
    {
        escapeUI.SetActive(true);
        _allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        if (_allAudioSources != null)
            foreach (var audioS in _allAudioSources)
                audioS.Stop();

        escapeAnim.SetBool(IsEscape, true);
        yield return new WaitForSeconds(1.5f);
        escapeText.GetComponent<TMP_Text>().text = "";
        escapeText.SetActive(true);
        Debug.Log("Get me the lines Taeko >:(");
        yield return new WaitForSeconds(2f);
        SaveSystem.SaveEndings(3);

        //STEAM ACHIEVEMENTS
        if(SteamManager.Initialized){
            SteamUserStats.SetAchievement("ENDING_3");
            SteamUserStats.StoreStats();
        }

        SceneManager.LoadScene("Credits");
    }
}
