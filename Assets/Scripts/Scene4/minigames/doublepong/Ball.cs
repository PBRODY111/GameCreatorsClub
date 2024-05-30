using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    public float initialVelocity = 8f; // Adjust the initial velocity as needed
    private Rigidbody2D rb;
    public bool isLaunched = false;
    private bool direction = false;
    private RectTransform rectTransform;
    [SerializeField] private DoublePong doublePong;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private AudioSource bounceAudio;
    [SerializeField] private AudioSource deathAudio;
    [SerializeField] private GameObject spaceBarText;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // No gravity for the ball
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; // Continuous collision detection
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Check if spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space) && !isLaunched)
        {
            // Set the initial velocity upwards
            if(direction){
                rb.velocity = new Vector2(0, -initialVelocity);
            } else{
                rb.velocity = new Vector2(0, initialVelocity);
            }
            isLaunched = true;
            spaceBarText.SetActive(false);
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("GameEdge"))
        {
            bounceAudio.Play();
            Debug.Log("Collided with GameEdge: " + collision.gameObject.name);

            // Calculate reflection based on collision normal
            Vector2 normal = collision.contacts[0].normal;
            Vector2 reflectedVelocity = Vector2.Reflect(rb.velocity, normal);

            // Ensure the reflected velocity maintains the initial speed
            reflectedVelocity = reflectedVelocity.normalized * initialVelocity;

            // Apply a slight change in angle to avoid perfect, repetitive trajectories
            float angleChange = Random.Range(-20f, 20f);
            reflectedVelocity = Quaternion.Euler(0, 0, angleChange) * reflectedVelocity;

            // Apply a bounce factor
            float bounceFactor = 0.9f; // Adjust the bounce factor as needed (0.9 means 90% bounce)
            reflectedVelocity *= bounceFactor;

            // Set the new velocity
            rb.velocity = reflectedVelocity;

            // Debug log to check collision
            Debug.Log("New Velocity: " + rb.velocity);
        } else if(collision.gameObject.CompareTag("Block")){
            // Calculate reflection based on collision normal
            Vector2 normal = collision.contacts[0].normal;
            Vector2 reflectedVelocity = Vector2.Reflect(rb.velocity, normal);

            // Ensure the reflected velocity maintains the initial speed
            reflectedVelocity = reflectedVelocity.normalized * initialVelocity;

            // Apply a slight change in angle to avoid perfect, repetitive trajectories
            float angleChange = Random.Range(-10f, 10f);
            reflectedVelocity = Quaternion.Euler(0, 0, angleChange) * reflectedVelocity;

            // Apply a bounce factor
            float bounceFactor = -0.9f; // Adjust the bounce factor as needed (0.9 means 90% bounce)
            reflectedVelocity *= bounceFactor;

            // Set the new velocity
            rb.velocity = -reflectedVelocity;
        } else{
            doublePong.lives--;
            deathAudio.Play();
            livesText.text = "LIVES: "+doublePong.lives;
            
            rb.velocity = Vector2.zero;
            spaceBarText.SetActive(true);

            isLaunched = false;
            direction = !direction;
            if(direction){
                MoveToPosition(new Vector3(0, 320, 0));
            } else{
                MoveToPosition(new Vector3(0, -320, 0));
            }

            if(doublePong.lives == 0){
                doublePong.ResetGame();
            }
        }
    }
    public void MoveToPosition(Vector3 newPosition)
    {
        rectTransform.anchoredPosition = newPosition;
    }

    public void ResetBall(){
        isLaunched = false;
        direction = !direction;
        if(direction){
            MoveToPosition(new Vector3(0, 320, 0));
        } else{
            MoveToPosition(new Vector3(0, -320, 0));
        }
    }
}
