using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class credits : MonoBehaviour
{
    [SerializeField] private AudioSource creditsAudio;
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
    }
}
