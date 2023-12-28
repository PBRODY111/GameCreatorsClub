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
    [SerializeField] private GameObject skipText;
    private bool canSkip = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(creditSequence());
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.E) && canSkip == true)
        {
            SceneManager.LoadScene(2);
        }
    }
    IEnumerator creditSequence()
    {
        // if first two endings wait 22 seconds, third ending on start, may have to change animations
        yield return new WaitForSeconds((float) 22.0);
        creditsAudio.Play();
        canSkip = true;
        skipText.SetActive(true);
        yield return new WaitForSeconds((float) 8);
        skipText.SetActive(false);
        yield return new WaitForSeconds((float) 160);
        img1.SetActive(true);
        yield return new WaitForSeconds((float) 1.0);
        img1.SetActive(false);
        SceneManager.LoadScene(2);
    }
}
