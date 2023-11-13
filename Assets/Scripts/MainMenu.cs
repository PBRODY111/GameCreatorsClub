using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Animator animator2;
    [SerializeField] private Animator animator3;
    [SerializeField] private Animator animator4;
    [SerializeField] private AudioSource audioData;
    [SerializeField] private AudioSource audioSource;
    // Start is called before the first frame update
    public void StartNew ()
    {
        StartCoroutine(fadeMusic());
        animator.SetBool("isStart", true);
        animator2.SetBool("isStart", true);
        animator3.SetBool("isStart", true);
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
