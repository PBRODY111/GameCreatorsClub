using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightFlicker : MonoBehaviour
{
    public bool isActive = false;
    [SerializeField] private bool isDark = true;
    [SerializeField] private GameObject light1;
    [SerializeField] private GameObject light2;
    [SerializeField] private AudioSource lightBuzz;
    [SerializeField] private Camera playerCam;
    [SerializeField] private Camera usCamera;
    [SerializeField] private Animator usAnimator;
    [SerializeField] private AudioSource jumpscareAudio;
    private static readonly int IsScared = Animator.StringToHash("isScared");
    // Start is called before the first frame update
    void Start()
    {
        playerCam.enabled = true;
        usCamera.enabled = false;
        StartCoroutine(FlickerLight());
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Zagreus" && isDark)
        {
            isDark = false;
            StartCoroutine(JumpscareSequence());
        }
    }

    public IEnumerator FlickerLight(){
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
            yield return new WaitForSeconds(Random.Range(4f, 8f));
            lightBuzz.Play();
            light1.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            light2.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            light2.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            light2.SetActive(false);
            isDark = true;
            yield return new WaitForSeconds(Random.Range(8f, 15f));
        }
    }
    public IEnumerator JumpscareSequence()
    {
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
