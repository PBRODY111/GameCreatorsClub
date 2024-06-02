using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class paradoxInhibitor : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private AudioSource switchAudio;
    [SerializeField] private AudioSource shutdownAudio;
    [SerializeField] private AudioSource systemAudio;
    [SerializeField] private GameObject intText;
    [SerializeField] private GameObject light;
    [SerializeField] private Animator escapeAnim;
    [SerializeField] private GameObject escapeText;
    [SerializeField] private GameObject escapeUI;
    private bool canInteract = true;
    private AudioSource[] _allAudioSources;
    private static readonly int IsEscape = Animator.StringToHash("isEscape");

    private void OnMouseExit()
    {
        intText.SetActive(false);
    }

    private void OnMouseOver()
    {
        intText.SetActive(IsWithinReach() && canInteract);
        if (Input.GetKeyDown(KeyCode.E) && IsWithinReach() && canInteract)
        {
            switchAudio.Play();
            transform.rotation = Quaternion.Euler(0, 0, -90);
            light.SetActive(false);
            canInteract = false;
            shutdownAudio.Play();
        }
    }

    IEnumerator Escape(){
        escapeUI.SetActive(true);
        escapeAnim.SetBool(IsEscape, true);
        yield return new WaitForSeconds(1.5f);
        _allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        if (_allAudioSources != null)
            foreach (var audioS in _allAudioSources)
                audioS.Stop();
        systemAudio.Play();
        escapeText.GetComponent<TMP_Text>().text = "";
        escapeText.SetActive(true);
        yield return new WaitForSeconds(6f);
        //SaveSystem.SaveEndings(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
