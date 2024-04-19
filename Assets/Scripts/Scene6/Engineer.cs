using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Scene6
{
    public class Engineer : MonoBehaviour
    {
        [SerializeField] private AudioSource paradoxAudio;
        [SerializeField] private AudioSource systemAudio;
        [SerializeField] private AudioClip [] paradoxLines;
        [SerializeField] private AudioClip [] systemLines;
        [SerializeField] private Animator escapeAnim;
        [SerializeField] private GameObject escapeText;
        [SerializeField] private GameObject escapeUI;

        private AudioSource[] _allAudioSources;
        private static readonly int IsEscape = Animator.StringToHash("isEscape");

        private void Start()
        {
            // ENDING 1
            StartCoroutine(Ending1());
        }

        IEnumerator Ending1(){
            yield return new WaitForSeconds(4f);
            paradoxAudio.clip = paradoxLines[0];
            paradoxAudio.Play();
            yield return new WaitForSeconds(7f);
            systemAudio.clip = systemLines[0];
            systemAudio.Play();
            yield return new WaitForSeconds(5f);
            StartCoroutine(Escape());
        }
        IEnumerator Escape(){
            escapeUI.SetActive(true);
            _allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            if (_allAudioSources != null)
                foreach (var audioS in _allAudioSources)
                    audioS.Stop();

            escapeAnim.SetBool(IsEscape, true);
            yield return new WaitForSeconds(1.5f);
            escapeText.GetComponent<TMP_Text>().text = "";
            escapeText.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}