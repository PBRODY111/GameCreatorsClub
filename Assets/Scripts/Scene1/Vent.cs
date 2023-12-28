using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Vent : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText;
    [SerializeField] private GameObject intText3;
    [SerializeField] private Animator cerAnimator;
    [SerializeField] private Animator escapeAnim;
    [SerializeField] private Attack1 attack1;
    [SerializeField] private GameObject cer;
    [SerializeField] private GameObject gas;
    [SerializeField] private GameObject escapeText;
    private AudioSource[] allAudioSources;
    public AudioSource growlAudio;
    public AudioSource doorSlam;
    public int unscrewed = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseOver()
    {
        if(unscrewed >= 4){
            intText3.GetComponent<TMP_Text>().text = "CROWBAR NEEDED TO INTERACT";
            intText3.SetActive(true);
            if(Input.GetMouseButtonDown(1) && IsWithinReach()){
                StartCoroutine(escapeFunc());
            }
        }
    }
    void OnMouseExit()
    {
        intText.SetActive(false);
        intText3.SetActive(false);
    }

    void Update()
    {
        if (unscrewed >= 4){
            PauseMenu.isPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            cerAnimator.SetBool("isLeaving", true);
            attack1.jumpscare = true;
            attack1.t = 0;
        }
    }

    IEnumerator escapeFunc(){
        cer.SetActive(false);
        gas.SetActive(false);
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach( AudioSource audioS in allAudioSources) {
            audioS.Stop();
        }
        escapeAnim.SetBool("isEscape", true);
        yield return new WaitForSeconds((float) 1.5);
        escapeText.SetActive(true);
        yield return new WaitForSeconds((float) 1.5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position) <= reach;
    }
}
