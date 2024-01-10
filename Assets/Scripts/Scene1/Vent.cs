using System.Collections;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene1
{
    public class Vent : MonoBehaviour
    {
        [SerializeField] private float reach;
        [SerializeField] private GameObject intText;
        [SerializeField] private GameObject intText3;
        [SerializeField] private Animator cerAnimator;
        [SerializeField] private Animator escapeAnim;
        [SerializeField] private Attack1 attack1;
        [SerializeField] private GameObject cer;
        [SerializeField] private GameObject gas;
        [SerializeField] private GameObject escapeText;
        [SerializeField] private GameObject escapeUI;
        public AudioSource growlAudio;
        public AudioSource doorSlam;
        public AudioSource footsteps;

        public int unscrewed;
        private AudioSource[] _allAudioSources;
        private static readonly int IsLeaving = Animator.StringToHash("isLeaving");
        private static readonly int IsEscape = Animator.StringToHash("isEscape");

        private void Update()
        {
            if (unscrewed >= 4)
            {
                PauseMenu.IsPaused = false;
                Cursor.lockState = CursorLockMode.Locked;
                cerAnimator.SetBool(IsLeaving, true);
                attack1.jumpscare = true;
                attack1.t = 0;
            }
        }

        private void OnMouseExit()
        {
            intText.SetActive(false);
            intText3.SetActive(false);
        }

        private void OnMouseOver()
        {
            if (unscrewed >= 4)
            {
                intText3.GetComponent<TMP_Text>().text = "CROWBAR NEEDED TO INTERACT";
                intText3.SetActive(true);

                if (Player.Player.Instance.EpicModeEnabled() && Input.GetMouseButtonDown(1))
                    StartCoroutine(EscapeFunc());

                if (Input.GetMouseButtonDown(1) && IsWithinReach() && Player.Player.Instance.IsHolding("Crowbar"))
                    StartCoroutine(EscapeFunc());
            }
        }

        public IEnumerator EscapeFunc()
        {
            escapeUI.SetActive(true);
            cer.SetActive(false);
            gas.SetActive(false);
            _allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            if (_allAudioSources != null)
                foreach (var audioS in _allAudioSources)
                    audioS.Stop();

            escapeAnim.SetBool(IsEscape, true);
            yield return new WaitForSeconds(1.5f);
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
}