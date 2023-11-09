using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    Animator animator;
    Animator animator2;
    AudioSource audioData;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start(){
        animator = GameObject.Find("cer").GetComponent<Animator>();
        animator2 = GameObject.Find("door").transform.GetChild(0).gameObject.GetComponent<Animator>();
        audioData = GameObject.Find("door").transform.GetChild(0).gameObject.GetComponent<AudioSource>();
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
    }
    /*
    void Update(){
        bool isStart = animator.GetBool("isStart");
        bool isStart2 = animator2.GetBool("isStart");
    }
    */
    public void StartNew ()
    {
        StartCoroutine(fadeMusic());
        animator.SetBool("isStart", true);
        animator2.SetBool("isStart", true);
        audioData.Play(0);
        StartCoroutine(loadStart());
        
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
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void QuitGame ()
    {
        Application.Quit();
    }
}
