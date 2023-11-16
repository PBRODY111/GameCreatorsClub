using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialLock : MonoBehaviour
{
    public GameObject dialLock;
    public GameObject dialUI;
    public float interactionDistance;
    public GameObject intText;
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
            if(hit.collider.gameObject.name == dialLock.name){
                intText.SetActive(true);
                if(Input.GetKeyDown(KeyCode.E)){
                    dialUI.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                }
            } else {
                intText.SetActive(false);
            }
        } else{
            intText.SetActive(false);
        }
        if(dialUI.activeInHierarchy){
            Cursor.lockState = CursorLockMode.None;
        } else{
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
