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
        yield return new WaitForSeconds((float) 22.0);
        creditsAudio.Play();
    }
}
