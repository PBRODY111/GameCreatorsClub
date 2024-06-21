using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Hunter : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 200f;
    [SerializeField] private float jumpForce = 350f;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Sprite[] sprites;      // Array of sprites to cycle through

    // Shooting
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletParent; 
    [SerializeField] private GameObject flash;
    [SerializeField] private Transform flashParent; 

    private Image imageComponent;  // The UI Image component to change
    private int currentSpriteIndex = 0; 

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private bool isGrounded;

    private void Awake()
    {
        imageComponent = GetComponent<Image>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void Start(){
        if (sprites.Length > 0)
        {
            StartCoroutine(SwitchImage());
        }
    }

    private void Update()
    {
        Move();
        Jump();
        LockRotation();
        if (Input.GetKeyDown(KeyCode.Space)){
            Instantiate(bullet, bulletParent);
            Instantiate(flash, flashParent);
        }
    }

    private void Move()
    {
        
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        isGrounded = Physics2D.IsTouchingLayers(coll, groundLayer);

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void LockRotation()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public IEnumerator SwitchImage()
    {
        while (true)
        {
            imageComponent.sprite = sprites[currentSpriteIndex];
            currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;  // Cycle through the array
            yield return new WaitForSeconds(0.1f);  // Wait for the specified interval
        }
    }
}