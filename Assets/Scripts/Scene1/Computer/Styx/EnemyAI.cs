using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scene1.Computer.Styx
{
    public class EnemyAI : MonoBehaviour
    {
        public float moveSpeed; // Increased speed of enemy movement (original speed multiplied by 3)
        public float chaseSpeedMultiplier = 0.5f; // Speed multiplier for chasing (slower than player)
        public Sprite[] walkingSprites; // Array of walking sprites for animation

        private int _currentSpriteIndex; // Index to track the current sprite in the array
        private Image _enemyImage; // Reference to the enemy's Image component

        private Transform _player; // Reference to the player's transform
        private readonly string _sceneToLoad = "WarningScene"; // Name of the scene to load
        private float _timer; // Timer for image changing interval
        
        private MoveUIWithKeys _moveUIWithKeys;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;

            _enemyImage = GetComponent<Image>();
            
            _moveUIWithKeys = FindObjectOfType<MoveUIWithKeys>();

            // Start the walking animation
            if (walkingSprites.Length > 0) _enemyImage.sprite = walkingSprites[0];
        }

        private void Update()
        {
            // If player is not found, stop executing the update loop
            if (_player == null)
            {
                Debug.LogWarning("Player not found!");
                return;
            }

            // Calculate direction towards the player
            Vector2 direction = (_player.position - transform.position).normalized;

            // Calculate movement speed for chasing (slower than player)
            var speed = moveSpeed * chaseSpeedMultiplier * Time.deltaTime;

            // Move towards the player's position
            transform.Translate(direction * speed);

            // Handle walking animation
            _timer += Time.deltaTime;
            if (!(_timer >= 0.2f)) return; // Interval for changing images (adjust as needed)

            _timer -= 0.2f;
            _currentSpriteIndex = (_currentSpriteIndex + 1) % walkingSprites.Length;
            _enemyImage.sprite = walkingSprites[_currentSpriteIndex];
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(_moveUIWithKeys.Epic) return;
            if (other.CompareTag("Player"))
                SceneManager.LoadScene(_sceneToLoad);
        }
    }
}