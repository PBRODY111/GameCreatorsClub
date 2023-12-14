using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class credits : MonoBehaviour
{
    [SerializeField] private AudioSource creditsAudio;
    [SerializeField] private GameObject img1;
    [SerializeField] private GameObject img2;
    [SerializeField] private GameObject img3;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(creditSequence());
    }

    // Update is called once per frame
    IEnumerator creditSequence()
    {
        // if first two endings wait 22 seconds, third ending on start, may have to change animations
        yield return new WaitForSeconds((float) 22.0);
        creditsAudio.Play();
        yield return new WaitForSeconds((float) 168);
        img1.SetActive(true);
        yield return new WaitForSeconds((float) 1.0);
        img1.SetActive(false);
        SceneManager.LoadScene(2);
    }
}
