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
            persephone.SetActive(true);
            pointsText.text = "0/1";
        }
    }

    private void UpdatePointsText()
    {
        if (pointsText != null) pointsText.text = "" + pointValue + "/15";
    }
}
