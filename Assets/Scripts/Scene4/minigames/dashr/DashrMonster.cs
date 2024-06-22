using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashrMonster : MonoBehaviour
{
    [SerializeField] private float timeToHide;
    [SerializeField] private DashR gameControl;

    [SerializeField] private Sprite[] sprites; 
    private Image imageComponent;  // The UI Image component to change
    private int currentSpriteIndex = 0; 
    // Start is called before the first frame update
    void Start()
    {
        gameControl = FindObjectOfType<DashR>();
        StartCoroutine(HideAfter());
        imageComponent = GetComponent<Image>();
        if (sprites.Length > 0)
        {
            StartCoroutine(SwitchImage());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block")){
            StartCoroutine(Death());
        }
        if(collision.gameObject.CompareTag("Player")){
            gameControl.Reset();
        }
    }

    void Update(){
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    IEnumerator HideAfter(){
        yield return new WaitForSeconds(timeToHide);
        Destroy(gameObject);
    }

    IEnumerator Death(){
        yield return new WaitForSeconds(0.1f);
        gameControl.IncreasePoint();
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
