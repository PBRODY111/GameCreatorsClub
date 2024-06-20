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
        [SerializeField] private GameObject rose;
        [SerializeField] private GameObject thermometer;
        [SerializeField] private GameObject wallText;

        private bool _canSkip;

        private void Start()
        {
            StartCoroutine(CreditSequence());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && _canSkip) SceneManager.LoadScene("TitleScene");
        }

        private IEnumerator CreditSequence()
        {
            SaveData data2 = SaveSystem.LoadEndings();
            if(data2 != null){
                if(data2.ending == 2){
                    thermometer.SetActive(false);
                } else if(data2.ending == 3){
                    rose.SetActive(false);
                    thermometer.SetActive(false);
                    wallText.SetActive(true);
                }
            }
            // if first two endings wait 22 seconds, third ending on start, may have to change animations
            yield return new WaitForSeconds(22f);
            creditsAudio.Play();
            _canSkip = true;
            skipText.SetActive(true);
            yield return new WaitForSeconds(8);
            skipText.SetActive(false);
            yield return new WaitForSeconds(160);
            if(data2 != null){
                if(data2.ending == 1){
                    img1.SetActive(true);
                    yield return new WaitForSeconds(1f);
                    img1.SetActive(false);
                    SceneManager.LoadScene("TitleScene");
                } else if(data2.ending == 2){
                    img2.SetActive(true);
                    yield return new WaitForSeconds(1f);
                    img2.SetActive(false);
                    SceneManager.LoadScene("TitleScene");
                } else if(data2.ending == 3){
                    img3.SetActive(true);
                    yield return new WaitForSeconds(1f);
                    img3.SetActive(false);
                    SceneManager.LoadScene("ThanksScene");
                }
            } else{
                img1.SetActive(true);
                yield return new WaitForSeconds(1f);
                img1.SetActive(false);
                SceneManager.LoadScene("TitleScene");
            }
        }
    }
}