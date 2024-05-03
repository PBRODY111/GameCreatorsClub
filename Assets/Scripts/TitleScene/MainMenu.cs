using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
        [SerializeField] private GameObject extraButton;
        [SerializeField] private GameObject options;
        [SerializeField] private GameObject cont;
        [SerializeField] private TMP_Text stars;
        [SerializeField] private TMP_Text roomNum;
        [SerializeField] private string nextRoom = "MainScene";
        public Dropdown resolutionDropdown;
        public AudioMixer masterMixer;
        private bool _extrasOn;

        private bool _optionsOn;

        private Resolution[] _resolutions;

        private void Start()
        {
            SaveSystem.SaveLevel(0, "1:00.00");
            SaveData data = SaveSystem.LoadMinigame();
            if(data != null){
                if(data.styx){
                    stars.color = Color.red;
                }
            }
            SaveData data1 = SaveSystem.LoadLevel();
            if(data1 == null){
                Debug.Log("nolevel");
                cont.SetActive(false);
            } else{
                Debug.Log(data1.level);
                if(data1.level == 0){
                    cont.SetActive(false);
                } else if(data1.level == 1){
                    roomNum.text = "Room 1";
                    nextRoom = "MainScene";
                } else if(data1.level == 2){
                    roomNum.text = "Room 2";
                    nextRoom = "Room2";
                } else if(data1.level == 3){
                    roomNum.text = "Room 3";
                    nextRoom = "Room3";
                } else if(data1.level == 4){
                    roomNum.text = "Room 4";
                    nextRoom = "Room4";
                } else if(data1.level == 5){
                    roomNum.text = "Room 5";
                    nextRoom = "Room5";
                } else if(data1.level == 6){
                    roomNum.text = "Room 6";
                    nextRoom = "Room6";
                }
            }
            SaveData data2 = SaveSystem.LoadEndings();
            if(data2 == null){
                stars.text = "";
                extraButton.SetActive(false);
            } else{
                if(data2.ending == 0){
                    stars.text = "";
                    extraButton.SetActive(false);
                } else if(data2.ending == 1){
                    stars.text = "*";
                } else if(data2.ending == 2){
                    stars.text = "**";
                } else if(data2.ending == 3){
                    stars.text = "***";
                } else{
                    stars.text = "";
                }
            }
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

        void FixedUpdate(){
            Cursor.visible = true;
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
            StartCoroutine(LoadTemp());
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
        private IEnumerator LoadTemp()
        {
            yield return new WaitForSeconds((float)1.5);
            animator4.SetBool(IsStart, true);
            yield return new WaitForSeconds((float)2.5);
            SceneManager.LoadScene(nextRoom);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}