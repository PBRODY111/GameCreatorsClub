using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed; // Increased speed of enemy movement (original speed multiplied by 3)
    public float chaseSpeedMultiplier = 0.5f; // Speed multiplier for chasing (slower than player)
    string sceneToLoad = "WarningScene"; // Name of the scene to load
    public Sprite[] walkingSprites; // Array of walking sprites for animation

    Transform player; // Reference to the player's transform
    Image enemyImage; // Reference to the enemy's Image component

    int currentSpriteIndex = 0; // Index to track the current sprite in the array
    float timer = 0f; // Timer for image changing interval

    void Start()
    {
        // Find the player object based on its tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Get the Image component of the enemy
        enemyImage = GetComponent<Image>();

        // Start the walking animation
        if (walkingSprites.Length > 0)
        {
            enemyImage.sprite = walkingSprites[0];
        }
    }

    void Update()
    {
        // If player is not found, stop executing the update loop
        if (player == null)
        {
            Debug.LogWarning("Player not found!");
            return;
        }

        // Calculate direction towards the player
        Vector2 direction = (player.position - transform.position).normalized;

        // Calculate movement speed for chasing (slower than player)
        float speed = moveSpeed * chaseSpeedMultiplier * Time.deltaTime;

        // Move towards the player's position
        transform.Translate(direction * speed);

        // Handle walking animation
        timer += Time.deltaTime;
        if (timer >= 0.2f) // Interval for changing images (adjust as needed)
        {
            timer -= 0.2f;
            currentSpriteIndex = (currentSpriteIndex + 1) % walkingSprites.Length;
            enemyImage.sprite = walkingSprites[currentSpriteIndex];
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player has entered the enemy's trigger
        if (other.CompareTag("Player"))
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
