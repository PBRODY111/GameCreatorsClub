using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuzzleFlash : MonoBehaviour
{
    [SerializeField] private Image uiImage;       // Reference to the UI Image component
    [SerializeField] private Sprite[] sprites;    // Array of sprites to cycle through
    [SerializeField] private float delay = 0.1f;  // Delay between sprite changes

    private void Start()
    {
        if (uiImage == null)
        {
            uiImage = GetComponent<Image>();
        }

        if (sprites.Length > 0)
        {
            StartCoroutine(ChangeSpritesAndDestroy());
        }
    }

    private IEnumerator ChangeSpritesAndDestroy()
    {
        foreach (var sprite in sprites)
        {
            uiImage.sprite = sprite;
            yield return new WaitForSeconds(delay);
        }

        // After cycling through all sprites, destroy the GameObject
        Destroy(gameObject);
    }
}
