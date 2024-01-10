using UnityEngine;

namespace Scene1.Computer.Styx
{
    public class Collectible : MonoBehaviour
    {
        public AudioSource otherAudioSource;
        private GameController _gameController;

        private void Start()
        {
            _gameController = FindObjectOfType<GameController>();
            if (_gameController == null) Debug.LogError("GameController not found in the scene.");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player") || _gameController == null) return;

            _gameController.IncrementPointValue();

            gameObject.SetActive(false);

            if (otherAudioSource != null && otherAudioSource.clip != null)
                otherAudioSource.PlayOneShot(otherAudioSource.clip);
        }
    }
}