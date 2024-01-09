using UnityEngine;

namespace Scene1.Computer.Styx
{
    public class Collectible : MonoBehaviour
    {
        public AudioSource otherAudioSource; // Reference to the AudioSource from another object
        private GameController _gameController; // Reference to the GameController

        private void Start()
        {
            // Find the GameController script in the scene
            _gameController = FindObjectOfType<GameController>();
            if (_gameController == null) Debug.LogError("GameController not found in the scene.");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check if the player has entered the collectible's trigger
            if (!other.CompareTag("Player") || _gameController == null) return;

            // Increment point value in GameController
            _gameController.IncrementPointValue();

            // Disable the collectible object
            gameObject.SetActive(false);

            // Play the audio from the specified AudioSource
            if (otherAudioSource != null && otherAudioSource.clip != null)
                otherAudioSource.PlayOneShot(otherAudioSource.clip);
        }
    }
}