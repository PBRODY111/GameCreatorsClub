using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scene1.Computer.Styx
{
    public class MoveUIWithKeys : MonoBehaviour
    {
        public float moveSpeed = 100f;
        public float animationInterval = 0.1f;
        public Sprite[] walkingSprites;
        public GameObject followerObject;
        [SerializeField] private GameObject secret;

        private int _currentSpriteIndex;
        private Image _imageComponent;
        private BoxCollider2D _myCollider;
        private RectTransform _rectTransform;
        private float _timer;

        internal bool Epic;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();

            _myCollider = GetComponent<BoxCollider2D>();

            _imageComponent = GetComponent<Image>();

            if (walkingSprites.Length > 0) _imageComponent.sprite = walkingSprites[0];
        }

        private void Update()
        {
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");
            
            if(Input.GetKeyDown(KeyCode.F3))
                Debug.Log("Epic mode will be changed here");
                //Epic = !Epic;
            
            if (Epic)
            {
                moveSpeed = 600f;
                animationInterval = 0.05f;
                followerObject.SetActive(false);
            }
            else
            {
                moveSpeed = 100f;
                animationInterval = 0.1f;
                followerObject.SetActive(true);
            }

            if (horizontalInput != 0 || verticalInput != 0)
            {
                var moveDirection = new Vector3(horizontalInput, verticalInput, 0f) * (moveSpeed * Time.deltaTime);

                var newPosition = _rectTransform.anchoredPosition3D + moveDirection;

                _rectTransform.anchoredPosition3D = newPosition;

                _timer += Time.deltaTime;
                if (_timer >= animationInterval) ChangeAnimation();
            }
            else ResetAnimation();

            if (followerObject != null) followerObject.transform.position = _rectTransform.position;
        }
        
        private void ResetAnimation()
        {
            _currentSpriteIndex = 0;
            _imageComponent.sprite = walkingSprites[_currentSpriteIndex];
            _timer = 0f;
        }

        private void ChangeAnimation()
        {
            _timer -= animationInterval;
            _currentSpriteIndex = (_currentSpriteIndex + 1) % walkingSprites.Length;
            _imageComponent.sprite = walkingSprites[_currentSpriteIndex];
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Respawn") && !Epic)
                SceneManager.LoadScene("WarningScene");
            else if (other.CompareTag("Finish"))
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