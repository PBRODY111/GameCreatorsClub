using System.Collections;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Room3Escape : MonoBehaviour
{
    [SerializeField] private AudioSource charlie;
    [SerializeField] private Animator escapeAnim;
    [SerializeField] private GameObject cer;
    [SerializeField] private GameObject escapeText;
    [SerializeField] private GameObject escapeUI;

    private AudioSource[] _allAudioSources;
    private static readonly int IsEscape = Animator.StringToHash("isEscape");
    // Start is called before the first frame update
    public IEnumerator endRoom3(){
        yield return new WaitForSeconds(3f);
        charlie.Play();
        yield return new WaitForSeconds(3f);
        StartCoroutine(EscapeFunc());
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
        SaveSystem.SaveLevel(4, Player.Player.Instance.GetTime());
        escapeText.GetComponent<TMP_Text>().text = "ESCAPED\nIn " + Player.Player.Instance.GetTime();
        escapeText.SetActive(true);
        yield return new WaitUntil(() => Input.anyKeyDown);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartEnd(){
        StartCoroutine(endRoom3());
    }
}
