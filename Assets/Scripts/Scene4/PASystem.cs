using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scene6;

public class PASystem : MonoBehaviour
{
    [SerializeField] private AudioSource paAudio;
    [SerializeField] private AudioClip [] myraLines;
    [SerializeField] private DoorCloseTrigger1 doorClose;
    [SerializeField] private LightFlicker lightFlicker;
    private bool line1 = false;
    private bool line2 = false;

    // Update is called once per frame
    void Update()
    {
        if(doorClose.hasEntered && !line1){
            line1 = true;
            StartCoroutine(Line1());
        }
        if(lightFlicker.leversOff == false && !line2){
            line2 = true;
            StartCoroutine(Line2());
        }
    }
    IEnumerator Line1(){
        yield return new WaitForSeconds(6f);
        paAudio.clip = myraLines[0];
        paAudio.Play();
    }
    IEnumerator Line2(){
        yield return new WaitForSeconds(1.5f);
        paAudio.clip = myraLines[1];
        paAudio.Play();
    }
}
