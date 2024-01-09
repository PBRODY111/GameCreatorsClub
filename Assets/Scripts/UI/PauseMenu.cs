using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private AudioSource clickAudio;
        public static bool IsPaused;
        [SerializeField] private GameObject options;
        public Dropdown resolutionDropdown;
        public AudioMixer masterMixer;
        private Resolution[] _resolutions;

        private bool _optionsOn;

        // Start is called before the first frame update
        private void Start()
        {
            pauseMenu.SetActive(false);
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
                {
                    currentResIndex = i;
                }
            }

            resolutionDropdown.AddOptions(opts);
            resolutionDropdown.value = currentResIndex;
            resolutionDropdown.RefreshShownValue();
        }

        // OPTIONS
        public void OptionsMenu()
        {
            clickAudio.Play();
            if (_optionsOn == false)
            {
                pauseMenu.SetActive(false);
                options.SetActive(true);
            }
            else
            {
                pauseMenu.SetActive(true);
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

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (IsPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }

        public void PauseGame()
        {
            clickAudio.Play();
            pauseMenu.SetActive(true);
            options.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            IsPaused = true;
        }

        public void ResumeGame()
        {
            clickAudio.Play();
            pauseMenu.SetActive(false);
            options.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            IsPaused = false;
        }

        public void ReturnToMenu()
        {
            clickAudio.Play();
            Time.timeScale = 1f;
            SceneManager.LoadScene(2);
            IsPaused = false;
        }
    }
}