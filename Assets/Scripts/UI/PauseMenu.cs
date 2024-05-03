using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

    public class PauseMenu : MonoBehaviour
    {
        public static bool IsPaused;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private AudioSource clickAudio;
        [SerializeField] private GameObject options;
        [SerializeField] private Player.PlayerCam playerCam;
        public Dropdown resolutionDropdown;
        public AudioMixer masterMixer;
        private bool _optionsOn;
        private Resolution[] _resolutions;

        // UI elements where saved values change
        public Slider masterSlid;
        public Slider musicSlid;
        public Slider sfxSlid;
        public Slider sensSlid;
        public Slider fovSlid;

        // these variables will be saved
        public float masterVol;
        public float musicVol;
        public float sfxVol;
        public float sensitivity;
        public float fov0;

        private void Start()
        {
            // get saved data
            SaveData data = SaveSystem.LoadOptions();
            if(data != null){
                masterMixer.SetFloat("MasterVol", data.masterVol);
                masterMixer.SetFloat("MusicVol", data.musicVol);
                masterMixer.SetFloat("SFXVol", data.sfxVol);
                Player.Player.Instance.ChangeFOV(data.fov);
                playerCam.sensX = data.sensitivity;
                playerCam.sensY = data.sensitivity;
            }

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
                    currentResIndex = i;
            }

            resolutionDropdown.AddOptions(opts);
            resolutionDropdown.value = currentResIndex;
            resolutionDropdown.RefreshShownValue();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (IsPaused)
                    ResumeGame();
                else
                    PauseGame();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Cursor.visible = true;
                Player.Player.Instance.UnlockCursor();
            }
        }

        // OPTIONS
        public void OptionsMenu()
        {
            clickAudio.Play();
            if (_optionsOn == false)
            {
                pauseMenu.SetActive(false);
                options.SetActive(true);
                //set sliders to saved
                SaveData data = SaveSystem.LoadOptions();
                if(data != null){
                    masterSlid.value = data.masterVol;
                    musicSlid.value = data.musicVol;
                    sfxSlid.value = data.sfxVol;
                    sensSlid.value = data.sensitivity;
                    fovSlid.value = data.fov;
                }
            }
            else
            {
                SaveSystem.SaveOptions(this);
                pauseMenu.SetActive(true);
                options.SetActive(false);
            }

            _optionsOn = !_optionsOn;
        }

        public void SetVolume(float volume)
        {
            masterMixer.SetFloat("MasterVol", volume);
            masterVol = volume;
        }

        public void SetMusic(float volume)
        {
            masterMixer.SetFloat("MusicVol", volume);
            musicVol = volume;
        }

        public void SetSfx(float volume)
        {
            masterMixer.SetFloat("SFXVol", volume);
            sfxVol = volume;
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

        public void SetFOV(float fov)
        {
            Player.Player.Instance.ChangeFOV(fov);
            fov0 = fov;
        }

        public void SetSensitivity(float sens)
        {
            playerCam.sensX = sens;
            playerCam.sensY = sens;
            sensitivity = sens;
        }

        public void PauseGame()
        {
            clickAudio.Play();
            Cursor.visible = true;
            pauseMenu.SetActive(true);
            options.SetActive(false);
            FreezeGame();
        }

        public void ResumeGame()
        {
            clickAudio.Play();
            pauseMenu.SetActive(false);
            options.SetActive(false);
            UnFreezeGame();
        }

        public void FreezeGame()
        {
            Player.Player.Instance.DisableMovement();
            Player.Player.Instance.UnlockCursor();
            Time.timeScale = 0f;
            IsPaused = true;
        }

        public void UnFreezeGame()
        {
            Player.Player.Instance.EnableMovement();
            Player.Player.Instance.LockCursor();
            Time.timeScale = 1f;
            IsPaused = false;
        }

        public void ReloadLevel()
        {
            clickAudio.Play();
            Time.timeScale = 1f;
            var currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
            IsPaused = false;
        }

        public void ReturnToMenu()
        {
            clickAudio.Play();
            Time.timeScale = 1f;
            SceneManager.LoadScene("TitleScene");
            IsPaused = false;
        }
    }