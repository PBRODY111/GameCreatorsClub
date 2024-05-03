using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroCutscene : MonoBehaviour
{
    [SerializeField] private AudioSource davis;
    [SerializeField] private AudioSource stephen;
    [SerializeField] private AudioSource dana;
    [SerializeField] private AudioSource audio1;
    [SerializeField] private AudioSource audio2;
    [SerializeField] private AudioClip [] davisLines;
    [SerializeField] private AudioClip [] stephenLines;
    [SerializeField] private AudioClip [] danaLines;
    [SerializeField] private AudioClip [] audio1Arr;
    [SerializeField] private AudioClip [] audio2Arr;
    [SerializeField] private VideoPlayer titleVid;
    [SerializeField] private RawImage videoImg;
    private AudioSource[] allAudioSources;
    [SerializeField] private GameObject monsters;
    [SerializeField] private GameObject people;
    private float intensity;
    
    private bool _canSkip;

    void Start()
    {
        SaveSystem.SaveLevel(1, "0:00.00");
        intensity = Mathf.Clamp(1.2f, 0f, 1f);
        RenderSettings.ambientIntensity = intensity;
        monsters.SetActive(false);
        people.SetActive(true);
        StartCoroutine(IntroClip());
        videoImg.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _canSkip) SceneManager.LoadScene("MainScene");
    }

    IEnumerator IntroClip() {
        _canSkip = true;
        yield return new WaitForSeconds(2f);
        davis.Play();
        yield return new WaitForSeconds(11f);
        stephen.Play();
        yield return new WaitForSeconds(1f);
        dana.Play();
        yield return new WaitForSeconds(2f);
        stephen.clip = stephenLines[1];
        stephen.Play();
        yield return new WaitForSeconds(4f);
        dana.clip = danaLines[1];
        dana.Play();
        yield return new WaitForSeconds(1.5f);
        davis.clip = davisLines[1];
        davis.Play();
        yield return new WaitForSeconds(5f);
        dana.clip = danaLines[2];
        dana.Play();
        yield return new WaitForSeconds(1.5f);
        // stop all sounds
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach( var audioS in allAudioSources) {
            audioS.Stop();
        }
        intensity = Mathf.Clamp(0.5f, 0f, 1f);
        RenderSettings.ambientIntensity = intensity;
        people.SetActive(false);
        monsters.SetActive(true);
        audio1.clip = audio1Arr[0];
        audio1.Play();
        yield return new WaitForSeconds(6f);
        audio2.clip = audio2Arr[0];
        audio2.Play();
        yield return new WaitForSeconds(5f);
        audio1.clip = audio1Arr[1];
        audio1.Play();
        yield return new WaitForSeconds(9f);
        audio2.clip = audio2Arr[2];
        audio2.Play();
        yield return new WaitForSeconds(6f);
        audio1.clip = audio1Arr[2];
        audio1.Play();
        yield return new WaitForSeconds(1f);
        audio2.clip = audio2Arr[1];
        audio2.Play();
        // start title sequence
        videoImg.enabled = true;
        titleVid.Play();
        yield return new WaitForSeconds(19f);
        SceneManager.LoadScene("MainScene");
    }
}
