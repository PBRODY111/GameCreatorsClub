using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Jumpscare : MonoBehaviour
{
    [SerializeField] private GameObject cer;
    [SerializeField] private Animator cerAnimator;
    [SerializeField] private AudioSource jumpscareAudio;
    [SerializeField] private GameObject intText3;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera cerCamera;
    [SerializeField] private float reach;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera.enabled = true;
        cerCamera.enabled = false;
    }

    void OnMouseOver()
    {
        intText3.GetComponent<TMP_Text>().text = "CROWBAR NEEDED TO INTERACT";
        intText3.SetActive(IsWithinReach());
        if(Input.GetMouseButtonDown(1) && IsWithinReach()){
            Debug.Log("JUMPSCARE!!");
            // this should only happen if the crowbar is used
            cer.transform.position = new Vector3(-8.5f, 0.5f, -4f);
            //cer.SetActive(true);
            playerModel.transform.position = new Vector3(-6.5f, 0f, -4f);
            StartCoroutine(jumpscareSequence());
        }
    }
    void OnMouseExit()
    {
        intText3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator jumpscareSequence()
    {
        mainCamera.enabled = false;
        cerCamera.enabled = true;
        cerAnimator.SetBool("isScared", true);
        jumpscareAudio.Play();
        yield return new WaitForSeconds((float) 1.5);
        SceneManager.LoadScene("GameOverScene");
    }

    bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position) <= reach;
    }
}
