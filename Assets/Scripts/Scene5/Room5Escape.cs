using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Room5Escape : MonoBehaviour
{
    [SerializeField] private Animator escapeAnim;
    [SerializeField] private GameObject cer;
    [SerializeField] private GameObject escapeText;
    [SerializeField] private GameObject escapeUI;
    private AudioSource[] _allAudioSources;
    private static readonly int IsEscape = Animator.StringToHash("isEscape");
    private bool hasEntered = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
        cer.SetActive(false);
        _allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        if (_allAudioSources != null)
            foreach (var audioS in _allAudioSources)
                audioS.Stop();

        escapeAnim.SetBool(IsEscape, true);
        yield return new WaitForSeconds(1.5f);
        SaveSystem.SaveLevel(6, Player.Player.Instance.GetTime());
        escapeText.GetComponent<TMP_Text>().text = "ESCAPED\nIn " + Player.Player.Instance.GetTime();
        escapeText.SetActive(true);
        yield return new WaitUntil(() => Input.anyKeyDown);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
