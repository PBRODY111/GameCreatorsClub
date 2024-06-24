using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastBug : MonoBehaviour
{
    [SerializeField] private DashR gameControl;
    [SerializeField] private float moveSpeed; // Speed at which the image moves left
    
    [SerializeField] private Sprite[] sprites; 
    private Image imageComponent;  // The UI Image component to change
    private int currentSpriteIndex = 0; 
    private RectTransform rectTransform;
    // Start is called before the first frame update
    void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        gameControl = FindObjectOfType<DashR>();
        imageComponent = GetComponent<Image>();
        if (sprites.Length > 0)
        {
            StartCoroutine(SwitchImage());
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rectTransform.anchoredPosition += Vector2.left * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block")){
            StartCoroutine(Death());
        }
        if(collision.gameObject.CompareTag("Player")){
            gameControl.Reset();
        }
        if (collision.gameObject.CompareTag("GameEdge")){
            Destroy(gameObject);
        }
    }

    IEnumerator Death(){
        Debug.Log("DEATH");
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    IEnumerator SwitchImage()
    {
        while (true)
        {
            imageComponent.sprite = sprites[currentSpriteIndex];
            currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;  // Cycle through the array
            yield return new WaitForSeconds(0.1f);  // Wait for the specified interval
        }
    }
}
