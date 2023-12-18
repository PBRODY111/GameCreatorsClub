using UnityEngine;
using UnityEngine.UI;

public class SafeDoor : MonoBehaviour
{
    [SerializeField] private GameObject intText;
    [SerializeField] private float[] dialValues;
    private float[] tempValues;
    [SerializeField] private float reach;
    [SerializeField] private GameObject dialUI;
    private Animator safeAnimator;
    private Slider slider;
    private float sliderPreviousValue;
    private int currentDialValueIndex = 0;
    private bool isUnlocked = false;
    [SerializeField] private AudioSource unlockAudio;

    void Awake()
    {
        slider = dialUI.GetComponentInChildren<Slider>();
        safeAnimator = GetComponentInChildren<Animator>();
        tempValues = new float[dialValues.Length];
        slider.onValueChanged.AddListener (delegate
        {
            if(IsWithinValue(sliderPreviousValue,slider.value,1f)) GetComponent<AudioSource>().Play();
            sliderPreviousValue = slider.value;
        });
    }
    void OnMouseOver()
    {
        if (!dialUI.activeSelf && !isUnlocked)
            intText.SetActive(IsWithinReach());
        if (Input.GetKeyDown(KeyCode.E) && !isUnlocked && IsWithinReach())
        {
            intText.SetActive(false);
            dialUI.SetActive(true);
            PauseMenu.isPaused = true;
            Cursor.lockState = dialUI.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
        }


    }
    
    void OnMouseExit(){
        if(!dialUI.activeSelf)
            intText.SetActive(false);
    }

    void Update()
    {

        if (dialUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                dialUI.SetActive(false);
                PauseMenu.isPaused = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (IsWithinValue(dialValues[currentDialValueIndex], slider.value, 2f))
                {
                    currentDialValueIndex++;
                    if (currentDialValueIndex >= dialValues.Length)
                    {
                        safeAnimator.SetBool("unlock", true);
                        unlockAudio.Play();
                        dialUI.SetActive(false);
                        PauseMenu.isPaused = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        isUnlocked = true;
                    }
                }
                else
                {
                    currentDialValueIndex = 0;
                }
                slider.value = 0;
            }
        }
    }

    bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position) <= reach;
    }

    bool IsWithinValue(float value, float actual,float deviation)
    {
        return actual >= value - deviation && actual <= value + deviation;
    }
}
