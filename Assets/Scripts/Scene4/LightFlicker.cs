using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class LightFlicker : MonoBehaviour
{
    public bool isActive = false;
    [SerializeField] private bool isDark = true;
    [SerializeField] private GameObject light1;
    [SerializeField] private GameObject light2;
    [SerializeField] private GameObject riveraLight;
    [SerializeField] private GameObject barricade;
    [SerializeField] private GameObject us;
    [SerializeField] private AudioSource lightBuzz;
    [SerializeField] private Camera playerCam;
    [SerializeField] private Camera usCamera;
    [SerializeField] private Animator usAnimator;
    [SerializeField] private Animator dividerAnimator;
    [SerializeField] private AudioSource jumpscareAudio;
    [SerializeField] private AudioSource startupAudio;
    [SerializeField] private AudioSource dividerAudio;
    [SerializeField] private bool isInTrigger = false;
    [SerializeField] private RiveraLever [] riveraLevers;
    [SerializeField] private Attack4Cer cer;
    public bool leversOff = true;
    private static readonly int IsScared = Animator.StringToHash("isScared");
    private static readonly int IsOpen = Animator.StringToHash("isOpen");
    private bool allFalse;
    // Start is called before the first frame update
    void Start()
    {
        playerCam.enabled = true;
        usCamera.enabled = false;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Zagreus")
        {
            isInTrigger = true;
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Zagreus")
        {
            isInTrigger = false;
        }
    }
    void Update(){
        allFalse = riveraLevers.All(lever => !lever.directionDown);
        if (allFalse && leversOff){
            isActive = false;
            leversOff = false;
            riveraLight.SetActive(true);
            us.SetActive(false);
            startupAudio.Play();
            StartCoroutine(LightsOn());
        }
        if (isInTrigger && isDark && leversOff){
            isDark = false;
            StartCoroutine(JumpscareSequence());
        }
    }

    public IEnumerator FlickerLight(){
        barricade.SetActive(false);
        while(isActive){
            lightBuzz.Play();
            light1.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            isDark = false;
            light2.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            light1.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            light1.SetActive(true);
            yield return new WaitForSeconds(Random.Range(4f, 5f)); // change to 4,8
            lightBuzz.Play();
            light1.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            light2.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            light2.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            light2.SetActive(false);
            isDark = true;
            yield return new WaitForSeconds(Random.Range(5f, 8f)); // change to 8,15
        }
    }
    public IEnumerator LightsOn(){
        lightBuzz.Play();
        light1.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        isDark = false;
        light2.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        light1.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        light1.SetActive(true);
        dividerAnimator.SetBool(IsOpen, true);
        dividerAudio.Play();
        StartCoroutine(cer.ScareSequence());
    }
    public IEnumerator JumpscareSequence()
    {
        SaveSystem.SaveHint("us","room4");
        Debug.Log("KILL!!");
        playerCam.enabled = false;
        usCamera.enabled = true;
        usAnimator.SetBool(IsScared, true);
        jumpscareAudio.Play();
        yield return new WaitForSeconds(2.25f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("GameOverScene");
    }
}
