using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Sentry : MonoBehaviour
{
    [SerializeField] private Camera playerCam;
    [SerializeField] private Camera sentryCam;
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText3;
    [SerializeField] private Animator sentryAnim;
    [SerializeField] private AudioSource sentryAudio;
    [SerializeField] private AudioSource jumpscareAudio;
    private static readonly int IsGrab = Animator.StringToHash("isGrab");
    private static readonly int IsJumpscare = Animator.StringToHash("isJumpscare");
    [SerializeField] private string item = "";
    
    private void Start(){
        playerCam.enabled = true;
        sentryCam.enabled = false;
    }

    private void OnMouseExit()
    {
        intText3.SetActive(false);
    }

    private void OnMouseOver()
    {
        if(IsWithinReach()){
            intText3.GetComponent<TMP_Text>().text = item+" NEEDED TO INTERACT";
            intText3.SetActive(true);

            if (Input.GetMouseButtonDown(1) && IsWithinReach()){
                if(Player.Player.Instance.IsHolding(item)){
                    // something
                } else{
                    // jumpscare the shit out of 'em
                    StartCoroutine(JumpscareSequence());
                }
            }
        }
    }

    public IEnumerator JumpscareSequence(){
        SaveSystem.SaveHint("paradox","room5");
        Debug.Log("KILL!!");
        sentryCam.enabled = true;
        sentryAnim.SetBool(IsJumpscare, true);
        jumpscareAudio.Play();
        yield return new WaitForSeconds(1.9f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("GameOverScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
