using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paddle : MonoBehaviour
{
    public RectTransform imageToMove; // Reference to the RectTransform of the UI Image
    public KeyCode moveLeftKey = KeyCode.A; // Key to move left
    public KeyCode moveRightKey = KeyCode.D; // Key to move right
    public float moveSpeed; // Speed of movement
    public float minX = -660f; // Minimum X position
    public float maxX = 660f; // Maximum X position

    private void Update()
    {
        float moveDirection = 0f;

        // Check for input and set move direction
        if (Input.GetKey(moveLeftKey))
        {
            moveDirection = -1f;
        }
        else if (Input.GetKey(moveRightKey))
        {
            moveDirection = 1f;
        }

        // Move the image
        if (moveDirection != 0f)
        {
            Vector2 newPosition = imageToMove.anchoredPosition + new Vector2(moveDirection * moveSpeed * Time.deltaTime, 0);

            // Clamp the position to ensure it stays within the defined limits
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

            imageToMove.anchoredPosition = newPosition;
        }
    }
}
