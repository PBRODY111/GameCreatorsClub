using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PreCutscene
{
    public class PreCutscene : MonoBehaviour
    {
        [SerializeField] private GameObject rose;
        [SerializeField] private GameObject zagreus;
        [SerializeField] private GameObject liar;
        [SerializeField] private new AudioSource audio;
        [SerializeField] private AudioSource audio2;
        private TypewriterUI _typewriterUi;

        private readonly string[] _text =
        {
            "Dad, can you hear me?",
            "Where are you?",
            "Please come home.",
            "I'm trapped in this nightmare.",
            "There's something after me.",
            "I don't know what it is.",
            "I'm so alone.",
            "I'm so scared.",
            "Please, help me.",
            "Help me.",
            "Please..."
        };

        private void Start()
        {
            _typewriterUi = transform.GetComponent<TypewriterUI>();
            StartCoroutine(CutScene());
        }

        private IEnumerator CutScene()
        {
            yield return new WaitForSeconds(0.5f);
            foreach (var line in _text)
            {
                _typewriterUi.SetText(line);
                _typewriterUi.Write();
                yield return new WaitForSeconds(_typewriterUi.GetTimeBetween() * line.Length + 1f);
            }

            audio.Stop();
            audio2.Play();
            rose.SetActive(true);
            zagreus.SetActive(false);
            _typewriterUi.SetText("There is no escape.");
            _typewriterUi.Write();
            yield return new WaitForSeconds(3f);
            liar.SetActive(true);
            yield return new WaitForSeconds(7f);
            liar.SetActive(false);
            rose.SetActive(false);
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}