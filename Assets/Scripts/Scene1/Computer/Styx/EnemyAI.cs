using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scene1.Computer.Styx
{
    public class EnemyAI : MonoBehaviour
    {
        public float moveSpeed;
        public float chaseSpeedMultiplier = 0.5f;
        public Sprite[] walkingSprites;

        private int _currentSpriteIndex;
        private Image _enemyImage;

        private Transform _player;
        private readonly string _sceneToLoad = "WarningScene";
        private float _timer;

        private MoveUIWithKeys _moveUIWithKeys;
        
        private readonly float _animationSpeed = 0.2f;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;

            _enemyImage = GetComponent<Image>();
            
            _moveUIWithKeys = FindObjectOfType<MoveUIWithKeys>();

            if (walkingSprites.Length > 0) _enemyImage.sprite = walkingSprites[0];
        }

        private void Update()
        {
            if (_player == null)
            {
                Debug.LogWarning("Player not found!");
                return;
            }

            Vector2 directionToPlayer = (_player.position - transform.position).normalized;

            var chaseSpeed = moveSpeed * chaseSpeedMultiplier * Time.deltaTime;
            
            transform.Translate(directionToPlayer * chaseSpeed);

            _timer += Time.deltaTime;
            if (_timer >= _animationSpeed) ChangeAnimation();
        }

        private void ChangeAnimation()
        {
            _timer -= _animationSpeed;
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