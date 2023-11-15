using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public GameObject radio;
    public float interactionDistance;
    public bool radioOn = true;
    public GameObject intText;
    [SerializeField] private AudioSource radioAudio;
    [SerializeField] private AudioSource clickAudio;
    // Start is called before the first frame update
    void Start()
    {
        intText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, interactionDistance)){
            //if(hit.collider.gameObject.name == radio.name){
            if(hit.collider.gameObject.tag == "radio"){
                Debug.Log("Radio Hovered!");
                intText.SetActive(true);
                if(Input.GetKeyDown(KeyCode.E)){
                    if(radioOn){
                        radioAudio.Pause();
                        clickAudio.Play(0);
                        radioOn = !radioOn;
                    } else {
                        clickAudio.Play(0);
                        radioAudio.Play(0);
                        radioOn = !radioOn;
                    }
                    Debug.Log("Radio Clicked");
                }
            } else {
                intText.SetActive(false);
            }
        } else{
            intText.SetActive(false);
        }
    }
}
