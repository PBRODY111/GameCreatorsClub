using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroCutscene : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioSource davis;
    [SerializeField] private AudioSource stephen;
    [SerializeField] private AudioClip [] davisLines;
    [SerializeField] private AudioClip [] stephenLines;
    [SerializeField] private VideoPlayer titleVid; // Reference to the VideoPlayer
    [SerializeField] private RawImage videoImg;
    private AudioSource[] allAudioSources;

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(IntroClip());
        videoImg.enabled = false;
    }

    IEnumerator IntroClip(){
        yield return new WaitForSeconds(2f);
        davis.Play();
        yield return new WaitForSeconds(11f);
        stephen.Play();
        yield return new WaitForSeconds(4f);
        stephen.clip = stephenLines[1];
        stephen.Play();
        yield return new WaitForSeconds(6f);
        davis.clip = davisLines[1];
        davis.Play();
        yield return new WaitForSeconds(6f);
        // stop all sounds
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach( AudioSource audioS in allAudioSources) {
            audioS.Stop();
        }
        // start title sequence
        videoImg.enabled = true;
        titleVid.Play();
        yield return new WaitForSeconds(17f);
        SceneManager.LoadScene("MainScene");
    }
}
