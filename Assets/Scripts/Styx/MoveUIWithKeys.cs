using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveUIWithKeys : MonoBehaviour
{
    public float moveSpeed = 100f; // Speed of movement
    public float animationInterval = 0.1f; // Interval for changing images (adjusted to 0.1)
    public Sprite[] walkingSprites; // Array of walking sprites for animation
    public GameObject followerObject; // Reference to another GameObject to follow this object's position
    RectTransform rectTransform; // Reference to the UI element's RectTransform
    BoxCollider2D myCollider; // Reference to the UI element's BoxCollider2D
    Image imageComponent; // Reference to the UI element's Image component
    [SerializeField] private GameObject secret;

    int currentSpriteIndex = 0; // Index to track the current sprite in the array
    float timer = 0f; // Timer for image changing interval

    void Start()
    {
        // Get the RectTransform component of the UI element
        rectTransform = GetComponent<RectTransform>();

        // Get the BoxCollider2D component of the UI element
        myCollider = GetComponent<BoxCollider2D>();

        // Get the Image component of the UI element
        imageComponent = GetComponent<Image>();

        // Initialize the image with the first sprite in the array
        if (walkingSprites.Length > 0)
        {
            imageComponent.sprite = walkingSprites[0];
        }
    }

    void Update()
    {
        // Calculate movement based on arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            // If arrow keys are pressed, perform movement and animation
            Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f) * moveSpeed * Time.deltaTime;

            // Calculate the new position
            Vector3 newPosition = rectTransform.anchoredPosition3D + moveDirection;

            // Move the object without collision checks
            rectTransform.anchoredPosition3D = newPosition;

            // Handle animation by changing sprites at regular intervals
            timer += Time.deltaTime;
            if (timer >= animationInterval)
            {
                timer -= animationInterval;
                currentSpriteIndex = (currentSpriteIndex + 1) % walkingSprites.Length;
                imageComponent.sprite = walkingSprites[currentSpriteIndex];
            }
        }
        else
        {
            // If no arrow keys are pressed, reset the animation sprite to the first element
            currentSpriteIndex = 0;
            imageComponent.sprite = walkingSprites[currentSpriteIndex];
            timer = 0f; // Reset the timer for consistent animation timing
        }

        // Update the followerObject's position to match this object's position
        if (followerObject != null)
        {
            followerObject.transform.position = rectTransform.position;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if this object enters the collider trigger of an object tagged "Respawn"
        if (other.CompareTag("Respawn"))
        {
            // Change the scene to "TitleScene"
            SceneManager.LoadScene("WarningScene");
        } else if (other.CompareTag("Finish"))
        {
            // Change the scene to "TitleScene"
            StartCoroutine(secretEnding());
        }
    }
    IEnumerator secretEnding(){
        secret.SetActive(true);
        yield return new WaitForSeconds((float) 2);
        SceneManager.LoadScene("WarningScene");
    }
}
