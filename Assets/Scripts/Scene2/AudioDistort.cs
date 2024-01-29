using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDistort : MonoBehaviour
{
    public AudioClip audioClip;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        // Start the audio playback coroutine
        StartCoroutine(AudioPlaybackRoutine());
    }

    IEnumerator AudioPlaybackRoutine()
    {
        while (true)
        {
            // Wait for 45 seconds
            yield return new WaitForSeconds(45f);

            // Play the audio clip for a random duration between 10 and 30 seconds
            float clipDuration = Random.Range(5f, 15f);
            audioSource.PlayOneShot(audioClip);
            yield return new WaitForSeconds(clipDuration);

            // Pause the audio clip
            audioSource.Pause();

            // Wait for another 45 seconds to 1 minute
            yield return new WaitForSeconds(Random.Range(45f, 60f));

            // Resume the audio clip
            audioSource.UnPause();

            // Wait for 10 to 30 seconds before repeating the loop
            yield return new WaitForSeconds(Random.Range(10f, 30f));
        }
    }
}

