using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialLock : MonoBehaviour
{
    public GameObject dialLock;
    public GameObject dialUI;
    public float interactionDistance;
    public GameObject intText;
    public Slider dialSlider;
    public string doorOpenAnimName;
    public float dialSliderGet;
    public float firstValue = 0;
    public float secondValue = 0;
    public float thirdValue = 0;
    public float tempValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        intText.SetActive(false);
        dialSlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(dialSlider.GetComponent<Slider>().value); // this gets the value of the dial slider
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

        if (!Input.GetMouseButton(0))
        {
            if(tempValue != 0){
                if(firstValue == 0){
                    firstValue = tempValue;
                } else if(secondValue == 0){
                    secondValue = tempValue;
                } else{
                    thirdValue = tempValue;
                }
                dialSlider.value = 0;
            } else if(firstValue != 0 && secondValue != 0 && thirdValue != 0) {
                dialUI.SetActive(false);
                if(firstValue >= 16 && firstValue <= 20 && secondValue >= 23 && secondValue <= 27 && thirdValue >= 3 && thirdValue <= 7){
                    Animator safeAnimator = dialLock.GetComponent<Animator>();
                    if(!safeAnimator.GetBool(doorOpenAnimName)){
                        safeAnimator.SetBool(doorOpenAnimName, true);
                    }
                }
                firstValue = secondValue = thirdValue = 0;
            }
        }
        Debug.Log("firstValue: "+firstValue);
        Debug.Log("secondValue: "+secondValue);
        Debug.Log("thirdValue: "+thirdValue);
    }

    void ValueChangeCheck()
	{
        tempValue = dialSlider.value;
	}
}
