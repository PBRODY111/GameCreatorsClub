using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class Credits : MonoBehaviour
    {
        [SerializeField] private AudioSource creditsAudio;
        [SerializeField] private GameObject img1;
        [SerializeField] private GameObject img2;
        [SerializeField] private GameObject img3;
        [SerializeField] private GameObject skipText;

        private bool _canSkip;

        private void Start()
        {
            StartCoroutine(CreditSequence());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && _canSkip) SceneManager.LoadScene(2);
        }

        private IEnumerator CreditSequence()
        {
            // if first two endings wait 22 seconds, third ending on start, may have to change animations
            yield return new WaitForSeconds(22f);
            creditsAudio.Play();
            _canSkip = true;
            skipText.SetActive(true);
            yield return new WaitForSeconds(8);
            skipText.SetActive(false);
            yield return new WaitForSeconds(160);
            img1.SetActive(true);
            yield return new WaitForSeconds(1f);
            img1.SetActive(false);
            SceneManager.LoadScene(2);
        }
    }
}