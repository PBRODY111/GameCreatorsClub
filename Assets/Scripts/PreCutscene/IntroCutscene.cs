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
    [SerializeField] private AudioClip [] davisLines;
    [SerializeField] private AudioClip [] stephenLines;
    [SerializeField] private AudioClip [] danaLines;
    [SerializeField] private VideoPlayer titleVid;
    [SerializeField] private RawImage videoImg;
    private AudioSource[] allAudioSources;
    [SerializeField] private GameObject monsters;
    [SerializeField] private GameObject people;
    private float intensity;
    
    private bool _canSkip;

    void Start()
    {
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
        yield return new WaitForSeconds(4.5f);
        // start title sequence
        videoImg.enabled = true;
        titleVid.Play();
        yield return new WaitForSeconds(17f);
        SceneManager.LoadScene("MainScene");
    }
}
