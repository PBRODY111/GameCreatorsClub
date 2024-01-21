using System.Collections;
using UnityEngine;

public class BellPlayer : MonoBehaviour
{
    public AudioSource bellAudio;
    public float intervalInSeconds = 300f; // 5 minutes in seconds

    void Start()
    {
        // InvokeRepeating starts the BellCoroutine method, repeating every intervalInSeconds seconds
        InvokeRepeating("BellCoroutine", 0f, intervalInSeconds);
    }

    void BellCoroutine()
    {
        // Start the coroutine to play the bell audio
        StartCoroutine(PlayBell());
    }

    IEnumerator PlayBell()
    {
        // Check if the bellAudio is not null and is not currently playing
        if (bellAudio != null && !bellAudio.isPlaying)
        {
            // Play the bell audio
            bellAudio.Play();

            // Wait for the audio clip duration
            yield return new WaitForSeconds(bellAudio.clip.length);

            // Stop the audio to ensure it doesn't overlap if the interval is less than the clip duration
            bellAudio.Stop();
        }
    }
}
