using UnityEngine;

public class Collectible : MonoBehaviour
{
    GameController gameController; // Reference to the GameController
    public AudioSource otherAudioSource; // Reference to the AudioSource from another object

    void Start()
    {
        // Find the GameController script in the scene
        gameController = FindObjectOfType<GameController>();
        if (gameController == null)
        {
            Debug.LogError("GameController not found in the scene.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player has entered the collectible's trigger
        if (other.CompareTag("Player") && gameController != null)
        {
            // Increment point value in GameController
            gameController.IncrementPointValue();

            // Disable the collectible object
            gameObject.SetActive(false);

            // Play the audio from the specified AudioSource
            if (otherAudioSource != null && otherAudioSource.clip != null)
            {
                otherAudioSource.PlayOneShot(otherAudioSource.clip);
            }
        }
    }
}
