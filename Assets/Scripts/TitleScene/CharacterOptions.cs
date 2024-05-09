using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterOptions : MonoBehaviour
{
    [SerializeField] private Sprite[] characters;
    [SerializeField] private int index = 0;
    [SerializeField] private Image charImage;
    [SerializeField] private TMP_Text charText;
    [SerializeField] private AudioSource clickAudio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void IndexUp(){
        clickAudio.Play();
        if(index==2){
            index=0;
            ChangeImage();
        } else{
            index++;
            ChangeImage();
        }
    }
    public void IndexDown(){
        clickAudio.Play();
        if(index==0){
            index=2;
            ChangeImage();
        } else{
            index--;
            ChangeImage();
        }
    }
    void ChangeImage(){
        if(index==0){
            charImage.sprite = characters[0];
            charText.text = "MONSTERS";
        } else if(index==1){
            charImage.sprite = characters[1];
            charText.text = "MYRA";
        } else if(index==2){
            charImage.sprite = characters[2];
            charText.text = "MISCELLANEOUS";
        }
    }
}
