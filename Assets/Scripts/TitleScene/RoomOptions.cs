using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoomOptions : MonoBehaviour
{
    [SerializeField] private Sprite[] rooms;
    [SerializeField] private int index = 0;
    [SerializeField] private Image charImage;
    [SerializeField] private TMP_Text charText;
    [SerializeField] private TMP_Text prText;
    [SerializeField] private AudioSource clickAudio;
    private SaveData data;
    // Start is called before the first frame update
    void Start()
    {
        data = SaveSystem.LoadLevel();
    }

    public void IndexUp(){
        clickAudio.Play();
        if(index==4){
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
            index=4;
            ChangeImage();
        } else{
            index--;
            ChangeImage();
        }
    }
    void ChangeImage(){
        if(index==0){
            charImage.sprite = rooms[0];
            charText.text = "ROOM 1";
            if(data != null && data.time1 != ""){
                prText.text = "PR: "+data.time1;
            } else{
                prText.text = "COMPLETE LEVEL TO GET RECORD";
            }
        } else if(index==1){
            charImage.sprite = rooms[1];
            charText.text = "ROOM 2";
            if(data != null && data.time2 != ""){
                prText.text = "PR: "+data.time2;
            } else{
                prText.text = "COMPLETE LEVEL TO GET RECORD";
            }
        } else if(index==2){
            charImage.sprite = rooms[2];
            charText.text = "ROOM 3";
            if(data != null && data.time3 != ""){
                prText.text = "PR: "+data.time3;
            } else{
                prText.text = "COMPLETE LEVEL TO GET RECORD";
            }
        } else if(index==3){
            charImage.sprite = rooms[3];
            charText.text = "ROOM 4";
            if(data != null && data.time4 != ""){
                prText.text = "PR: "+data.time4;
            } else{
                prText.text = "COMPLETE LEVEL TO GET RECORD";
            }
        } else if(index==4){
            charImage.sprite = rooms[4];
            charText.text = "ROOM 5";
            if(data != null && data.time5 != ""){
                prText.text = "PR: "+data.time5;
            } else{
                prText.text = "COMPLETE LEVEL TO GET RECORD";
            }
        }
    }
}
