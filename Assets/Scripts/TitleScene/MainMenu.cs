using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
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
    private bool extrasOn = false;
    private bool optionsOn = false;
    // Start is called before the first frame update
    Resolution[] resolutions;
    void Start(){
        Cursor.lockState = CursorLockMode.None;
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

    public void StartNew ()
    {
        click.Play();
        StartCoroutine(fadeMusic());
        animator.SetBool("isStart", true);
        animator2.SetBool("isStart", true);
        animator3.SetBool("isStart", true);
        audioData.Play(0);
        StartCoroutine(loadStart());
        
    }
    public void Continue ()
    {
        click.Play();
        StartCoroutine(fadeMusic());
        animator.SetBool("isStart", true);
        animator2.SetBool("isStart", true);
        animator3.SetBool("isStart", true);
        audioData.Play(0);
        StartCoroutine(loadStart());
        
    }

    // OPTIONS
    public void OptionsMenu(){
        click.Play();
        if(optionsOn == false){
            mainCanvas.SetActive(false);
            options.SetActive(true);
        }else{
            mainCanvas.SetActive(true);
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

    // EXTRAS
    public void ExtrasMenu(){
        click.Play();
        if(extrasOn == false){
            mainCanvas.SetActive(false);
            extras.SetActive(true);
        }else{
            mainCanvas.SetActive(true);
            extras.SetActive(false);
        }
        extrasOn = !extrasOn;
    }

    IEnumerator fadeMusic(){
        float startVolume = audioSource.volume;
 
        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / 3;
 
            yield return null;
        }
 
        audioSource.Stop ();
        audioSource.volume = startVolume;
    }
    IEnumerator loadStart()
    {
        yield return new WaitForSeconds((float) 1.5);
        animator4.SetBool("isStart", true);
        yield return new WaitForSeconds((float) 2.5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void QuitGame ()
    {
        Application.Quit();
    }
}
