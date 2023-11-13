using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public float interactionDistance;
    public GameObject intText;
    public string doorOpenAnimName;
    AudioSource doorAudio;
    // Start is called before the first frame update
    void Start()
    {
        intText.SetActive(false);
        doorAudio = GameObject.Find("Sounds").transform.GetChild(1).gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, interactionDistance)){
            if(hit.collider.gameObject.tag == "door"){
                //GameObject doorParent = hit.collider.transform.root.gameObject;
                GameObject doorParent = hit.collider.transform.gameObject;
                Animator doorAnim = doorParent.GetComponent<Animator>();
                intText.SetActive(true);
                if(Input.GetKeyDown(KeyCode.E)){
                    if(!doorAnim.GetBool(doorOpenAnimName)){
                        doorAnim.SetBool(doorOpenAnimName, true);
                        doorAudio.pitch = (float) 1.1;
                        doorAudio.Play(0);
                    }else if(doorAnim.GetBool(doorOpenAnimName)){
                        doorAnim.SetBool(doorOpenAnimName, false);
                        //doorAudio.pitch = (float) -1.1;
                        doorAudio.Play(0);
                    }
                }
            } else {
                intText.SetActive(false);
            }
        } else{
            intText.SetActive(false);
        }
    }
}
