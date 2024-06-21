using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 500f; // Speed at which the image moves
    [SerializeField] private GameObject bulletTarget;

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        bulletTarget = GameObject.Find("BulletTarget");
        rectTransform.position = bulletTarget.transform.position;
    }

    private void Update()
    {
        MoveRight();
    }

    private void MoveRight()
    {
        // Move the image to the right
        rectTransform.anchoredPosition += new Vector2(moveSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has the tag "GameEdge"
        if (collision.CompareTag("GameEdge") || collision.CompareTag("Respawn"))
        {
            Destroy(gameObject);
        }
    }
}
