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
        [SerializeField] private AudioSource jumpscareAudio;
        [SerializeField] private AudioClip [] paradoxLines;
        [SerializeField] private AudioClip [] systemLines;
        [SerializeField] private Animator escapeAnim;
        [SerializeField] private Animator engineerAnim;
        [SerializeField] private GameObject escapeText;
        [SerializeField] private GameObject escapeUI;
        [SerializeField] private bool isActive = false;
        [SerializeField] private Vector3 startPosition;
        [SerializeField] private float timeToReachTarget;
        private Quaternion _lookRotation;
        public float t;
        public Vector3 target;

        private AudioSource[] _allAudioSources;
        private static readonly int IsEscape = Animator.StringToHash("isEscape");
        private static readonly int IsScared = Animator.StringToHash("isScared");

        private void Start()
        {
            // ENDING 1
            StartCoroutine(Ending1());
        }

        void Update(){
            if(isActive){
                target = new Vector3(Player.Player.Instance.transform.position.x, gameObject.transform.position.y, Player.Player.Instance.transform.position.z);
                t += Time.deltaTime / timeToReachTarget;
                var thisTransform = gameObject.transform;
                startPosition = thisTransform.position;
                thisTransform.position = Vector3.Lerp(startPosition, target, t);
                _lookRotation = Quaternion.LookRotation(target);
                thisTransform.LookAt(target);

                // Check if the distance is within the threshold
                float distance = Vector3.Distance(transform.position, target);
            }
        }

        IEnumerator Ending1(){
            yield return new WaitForSeconds(4f);
            paradoxAudio.clip = paradoxLines[0];
            paradoxAudio.Play();
            yield return new WaitForSeconds(7f);
            StartCoroutine(Escape());
        }
        IEnumerator Escape(){
            escapeUI.SetActive(true);
            escapeAnim.SetBool(IsEscape, true);
            isActive = true;
            jumpscareAudio.Play();
            engineerAnim.SetBool(IsScared, true);
            yield return new WaitForSeconds(1.5f);
            _allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            if (_allAudioSources != null)
                foreach (var audioS in _allAudioSources)
                    audioS.Stop();
            systemAudio.clip = systemLines[0];
            systemAudio.Play();
            escapeText.GetComponent<TMP_Text>().text = "";
            escapeText.SetActive(true);
            yield return new WaitForSeconds(6f);
            SaveSystem.SaveEndings(1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}