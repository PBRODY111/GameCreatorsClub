using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using PreCutscene;
using Steamworks;

namespace Scene1.Computer.Styx
{
    public class Inhibitor : MonoBehaviour
    {
        public GameObject intText;
        private bool ended = true;

        private readonly string[] _text =
        {
            "You shouldn't be doing this..."
        };

        private TypewriterUI _typewriterUi;

        private void Start()
        {
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _typewriterUi = transform.GetComponent<TypewriterUI>();
            if (other.CompareTag("Player") && intText != null)
                intText.SetActive(true);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player") && intText != null)
                intText.SetActive(false);
        }

        void Update()
        {
            if (intText.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.E) && ended)
                {
                    StartCoroutine(CutScene());
                    ended = false;
                }
            }
        }

        private IEnumerator CutScene()
        {
            yield return new WaitForSeconds(0.5f);
            SaveSystem.SaveMinigame("styx");
            foreach (var line in _text)
            {
                _typewriterUi.SetText(line);
                _typewriterUi.Write();
                yield return new WaitForSeconds(_typewriterUi.GetTimeBetween() * line.Length + 1f);
            }
            yield return new WaitForSeconds(2f);
            //STEAM ACHIEVEMENTS
            if(SteamManager.Initialized){
                SteamUserStats.SetAchievement("STYX");
                SteamUserStats.StoreStats();
            }
            SceneManager.LoadScene("WarningScene");
        }
    }
}