using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Escape4 : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText;
    private bool isLeave = false;
    [SerializeField] private Animator escapeAnim;
    [SerializeField] private GameObject cer;
    [SerializeField] private GameObject escapeText;
    [SerializeField] private GameObject escapeUI;

    private AudioSource[] _allAudioSources;
    private static readonly int IsEscape = Animator.StringToHash("isEscape");
    // Start is called before the first frame update
    private void OnMouseExit()
    {
        intText.SetActive(false);
    }

    private void OnMouseOver()
    {
        intText.SetActive(IsWithinReach() && !isLeave);
        if (Input.GetKeyDown(KeyCode.E) && IsWithinReach() && !isLeave)
        {
            isLeave = true;
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
        SaveSystem.SaveLevel(5, Player.Player.Instance.GetTime());
        escapeText.GetComponent<TMP_Text>().text = "ESCAPED\nIn " + Player.Player.Instance.GetTime();
        escapeText.SetActive(true);
        yield return new WaitUntil(() => Input.anyKeyDown);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
