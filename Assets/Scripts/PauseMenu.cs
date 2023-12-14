using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private AudioSource clickAudio;
    public static bool isPaused = false;
    [SerializeField] private GameObject options;
    public Dropdown resolutionDropdown;
    public AudioMixer masterMixer;
    Resolution[] resolutions;
    private bool optionsOn = false;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResIndex = 0;
        for(int i=0; i<resolutions.Length; i++){
            string option = resolutions[i].width+"x"+resolutions[i].height;
            options.Add(option);
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height){
                currentResIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
    }

    // OPTIONS
    public void OptionsMenu(){
        clickAudio.Play();
        if(optionsOn == false){
            pauseMenu.SetActive(false);
            options.SetActive(true);
        }else{
            pauseMenu.SetActive(true);
            options.SetActive(false);
        }
        optionsOn = !optionsOn;
    }
    public void SetVolume(float volume){
        masterMixer.SetFloat("MasterVol", volume);
    }
    public void SetMusic(float volume){
        masterMixer.SetFloat("MusicVol", volume);
    }
    public void SetSFX(float volume){
        masterMixer.SetFloat("SFXVol", volume);
    }
    public void SetQuality(int qualityIndex){
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullscreen(bool isFull){
        Screen.fullScreen = isFull;
    }
    public void SetResolution(int resolutionIndex){
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1)){
            Debug.Log("HII");
            if(isPaused){
                ResumeGame();
            } else{
                PauseGame();
            }
        }
    }

    public void PauseGame(){
        clickAudio.Play();
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void ResumeGame(){
        clickAudio.Play();
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void ReturnToMenu(){
        clickAudio.Play();
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
        isPaused = false;
    }
}
