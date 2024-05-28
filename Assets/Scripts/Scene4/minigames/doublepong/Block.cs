using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Block : MonoBehaviour
{
    [SerializeField] private AudioSource breakAudio;
    [SerializeField] private Sprite currentImageSprite;
    [SerializeField] private Sprite newImageSprite; // Assign the new image sprite in the Inspector
    [SerializeField] private Image imageComponent;
    [SerializeField] private DoublePong doublePong;
    [SerializeField] private GameObject level1;
    [SerializeField] private GameObject level2;
    [SerializeField] private GameObject level3;
    [SerializeField] private TMP_Text levelText;
    // Start is called before the first frame update
    void Start()
    {
        imageComponent = GetComponent<Image>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(BreakBlock());
        }
    }

    IEnumerator BreakBlock(){
        breakAudio.Play();
        imageComponent.sprite = newImageSprite;
        yield return new WaitForSeconds(0.5f);
        doublePong.broken--;
        if(doublePong.broken == 0){
            doublePong.level++;
            levelText.text = "LEVEL: "+doublePong.level;
            if(doublePong.level == 2){
                level2.SetActive(true);
                doublePong.broken = 38;
                level1.SetActive(false);
            } else if (doublePong.level == 3){
                level3.SetActive(true);
                doublePong.broken = 57;
                level2.SetActive(false);
            } else{
                doublePong.GameWin();
            }
        }
        imageComponent.sprite = currentImageSprite;
        gameObject.SetActive(false);
    }
}
