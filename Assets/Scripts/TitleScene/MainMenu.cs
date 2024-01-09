using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TitleScene
{
    public class MainMenu : MonoBehaviour
    {
        private static readonly int IsStart = Animator.StringToHash("isStart");
        [SerializeField] private Animator animator;
        [SerializeField] private Animator animator2;
        [SerializeField] private Animator animator3;
        [SerializeField] private Animator animator4;
        [SerializeField] private AudioSource audioData;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioSource click;
        [SerializeField] private GameObject mainCanvas;
        [SerializeField] private GameObject extras;
        [SerializeField] private GameObject options;
        public Dropdown resolutionDropdown;
        public AudioMixer masterMixer;
        private bool _extrasOn;

        private bool _optionsOn;

        // Start is called before the first frame update
        private Resolution[] _resolutions;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            _resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();
            var opts = new List<string>();
            var currentResIndex = 0;
            for (var i = 0; i < _resolutions.Length; i++)
            {
                var option = _resolutions[i].width + "x" + _resolutions[i].height;
                opts.Add(option);
                if (_resolutions[i].width == Screen.currentResolution.width &&
                    _resolutions[i].height == Screen.currentResolution.height)
                    currentResIndex = i;
            }

            resolutionDropdown.AddOptions(opts);
            resolutionDropdown.value = currentResIndex;
            resolutionDropdown.RefreshShownValue();
        }

        public void StartNew()
        {
            click.Play();
            StartCoroutine(FadeMusic());
            animator.SetBool(IsStart, true);
            animator2.SetBool(IsStart, true);
            animator3.SetBool(IsStart, true);
            audioData.Play(0);
            StartCoroutine(LoadStart());
        }

        public void Continue()
        {
            click.Play();
            StartCoroutine(FadeMusic());
            animator.SetBool(IsStart, true);
            animator2.SetBool(IsStart, true);
            animator3.SetBool(IsStart, true);
            audioData.Play(0);
            StartCoroutine(LoadStart());
        }

        // OPTIONS
        public void OptionsMenu()
        {
            click.Play();
            if (_optionsOn == false)
            {
                mainCanvas.SetActive(false);
                options.SetActive(true);
            }
            else
            {
                mainCanvas.SetActive(true);
                options.SetActive(false);
            }

            _optionsOn = !_optionsOn;
        }

        public void SetVolume(float volume)
        {
            masterMixer.SetFloat("MasterVol", volume);
        }

        public void SetMusic(float volume)
        {
            masterMixer.SetFloat("MusicVol", volume);
        }

        public void SetSfx(float volume)
        {
            masterMixer.SetFloat("SFXVol", volume);
        }

        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }

        public void SetFullscreen(bool isFull)
        {
            Screen.fullScreen = isFull;
        }

        public void SetResolution(int resolutionIndex)
        {
            var resolution = _resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        // EXTRAS
        public void ExtrasMenu()
        {
            click.Play();
            if (_extrasOn == false)
            {
                mainCanvas.SetActive(false);
                extras.SetActive(true);
            }
            else
            {
                mainCanvas.SetActive(true);
                extras.SetActive(false);
            }

            _extrasOn = !_extrasOn;
        }

        private IEnumerator FadeMusic()
        {
            var startVolume = audioSource.volume;

            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / 3;

                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = startVolume;
        }

        private IEnumerator LoadStart()
        {
            yield return new WaitForSeconds((float)1.5);
            animator4.SetBool(IsStart, true);
            yield return new WaitForSeconds((float)2.5);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}