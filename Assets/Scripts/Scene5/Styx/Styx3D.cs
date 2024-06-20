using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Styx3D : MonoBehaviour
{
    public int pointValue;
    public GameObject textObject;
    public TextMeshProUGUI pointsText;
    public AudioSource pointAudio;
    public AudioSource styxAudio;
    [SerializeField] private GameObject persephone;
    [SerializeField] private GameObject board1;
    [SerializeField] private GameObject board2;
    private bool isDone = false;
    // Start is called before the first frame update
    void Start()
    {
        SaveData data2 = SaveSystem.LoadEndings();
        if(data2 != null){
            if(data2.ending < 2){
                gameObject.SetActive(false);
            }
        } else{
            gameObject.SetActive(false);
        }
        // if already got second ending
        textObject.SetActive(true);

        pointValue = 0;
    }

    public void IncrementPointValue()
    {
        if (pointValue < 15)
        {
            pointValue++;
            pointAudio.Play();
            UpdatePointsText();
        }
        if (pointValue == 15){
            styxAudio.Stop();
            isDone = true;
            pointsText.text = "0/1";
        }
    }

    private void UpdatePointsText()
    {
        if (pointsText != null) pointsText.text = "" + pointValue + "/15";
    }

    void Update(){
        if(isDone && !board1.activeSelf && !board2.activeSelf && !persephone.activeSelf){
            persephone.SetActive(true);
        }
    }
}
