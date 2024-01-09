using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scene1.Computer.Styx
{
    public class MoveUIWithKeys : MonoBehaviour
    {
        public float moveSpeed = 100f; // Speed of movement
        public float animationInterval = 0.1f; // Interval for changing images (adjusted to 0.1)
        public Sprite[] walkingSprites; // Array of walking sprites for animation
        public GameObject followerObject; // Reference to another GameObject to follow this object's position
        [SerializeField] private GameObject secret;

        private int _currentSpriteIndex; // Index to track the current sprite in the array
        private Image _imageComponent; // Reference to the UI element's Image component
        private BoxCollider2D _myCollider; // Reference to the UI element's BoxCollider2D
        private RectTransform _rectTransform; // Reference to the UI element's RectTransform
        private float _timer; // Timer for image changing interval

        private void Start()
        {
            // Get the RectTransform component of the UI element
            _rectTransform = GetComponent<RectTransform>();

            // Get the BoxCollider2D component of the UI element
            _myCollider = GetComponent<BoxCollider2D>();

            // Get the Image component of the UI element
            _imageComponent = GetComponent<Image>();

            // Initialize the image with the first sprite in the array
            if (walkingSprites.Length > 0) _imageComponent.sprite = walkingSprites[0];
        }

        private void Update()
        {
            // Calculate movement based on arrow keys
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");

            if (Player.Player.Instance.EpicModeEnabled())
            {
                moveSpeed = 300f;
                followerObject.SetActive(false);
            }

            if (horizontalInput != 0 || verticalInput != 0)
            {
                // If arrow keys are pressed, perform movement and animation
                var moveDirection = new Vector3(horizontalInput, verticalInput, 0f) * (moveSpeed * Time.deltaTime);

                // Calculate the new position
                var newPosition = _rectTransform.anchoredPosition3D + moveDirection;

                // Move the object without collision checks
                _rectTransform.anchoredPosition3D = newPosition;

                // Handle animation by changing sprites at regular intervals
                _timer += Time.deltaTime;
                if (_timer >= animationInterval)
                {
                    _timer -= animationInterval;
                    _currentSpriteIndex = (_currentSpriteIndex + 1) % walkingSprites.Length;
                    _imageComponent.sprite = walkingSprites[_currentSpriteIndex];
                }
            }
            else
            {
                // If no arrow keys are pressed, reset the animation sprite to the first element
                _currentSpriteIndex = 0;
                _imageComponent.sprite = walkingSprites[_currentSpriteIndex];
                _timer = 0f; // Reset the timer for consistent animation timing
            }

            // Update the followerObject's position to match this object's position
            if (followerObject != null) followerObject.transform.position = _rectTransform.position;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check if this object enters the collider trigger of an object tagged "Respawn"
            if (other.CompareTag("Respawn"))
                // Change the scene to "TitleScene"
                SceneManager.LoadScene("WarningScene");
            else if (other.CompareTag("Finish"))
                // Change the scene to "TitleScene"
                StartCoroutine(SecretEnding());
        }

        private IEnumerator SecretEnding()
        {
            secret.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene("WarningScene");
        }
    }
}