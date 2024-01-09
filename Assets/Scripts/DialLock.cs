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
    public float firstValue;
    public float secondValue;
    public float thirdValue;

    public float tempValue;

    // Start is called before the first frame update
    private void Start()
    {
        intText.SetActive(false);
        dialSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log(dialSlider.GetComponent<Slider>().value); // this gets the value of the dial slider
        var transform1 = transform;
        var ray = new Ray(transform1.position, transform1.forward);
        if (Physics.Raycast(ray, out var hit, interactionDistance))
        {
            if (hit.collider.gameObject.name == dialLock.name)
            {
                intText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    dialUI.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                }
            }
            else
            {
                //intText.SetActive(false);
            }
        }
        else
        {
            //intText.SetActive(false);
        }

        if (dialUI.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (!Input.GetMouseButton(0))
        {
            if (tempValue != 0)
            {
                if (firstValue == 0)
                {
                    firstValue = tempValue;
                }
                else if (secondValue == 0)
                {
                    secondValue = tempValue;
                }
                else
                {
                    thirdValue = tempValue;
                }

                dialSlider.value = 0;
            }
            else if (firstValue != 0 && secondValue != 0 && thirdValue != 0)
            {
                dialUI.SetActive(false);
                if (firstValue >= 16 && firstValue <= 20 && secondValue >= 23 && secondValue <= 27 && thirdValue >= 3 &&
                    thirdValue <= 7)
                {
                    var safeAnimator = dialLock.GetComponent<Animator>();
                    if (!safeAnimator.GetBool(doorOpenAnimName))
                    {
                        safeAnimator.SetBool(doorOpenAnimName, true);
                    }
                }

                firstValue = secondValue = thirdValue = 0;
            }
        }
        //Debug.Log("firstValue: "+firstValue);
        //Debug.Log("secondValue: "+secondValue);
        //Debug.Log("thirdValue: "+thirdValue);
    }

    private void ValueChangeCheck()
    {
        tempValue = dialSlider.value;
    }
}