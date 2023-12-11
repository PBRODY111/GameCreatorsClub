using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jumpscare : MonoBehaviour
{
    [SerializeField] private GameObject cer;
    [SerializeField] private Animator cerAnimator;
    [SerializeField] private AudioSource jumpscareAudio;
    [SerializeField] private GameObject intText3;
    [SerializeField] private float reach;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseOver()
    {
        intText3.SetActive(IsWithinReach());
        if(Input.GetMouseButtonDown(1) && IsWithinReach()){
            Debug.Log("JUMPSCARE!!");
            // this should only happen if the crowbar is used
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
    IEnumerator jumpscareSequence()
    {
        cer.transform.position = new Vector3(-8.5f, 0f, -4f);
        cerAnimator.SetBool("isScared", true);
        jumpscareAudio.Play();
        yield return new WaitForSeconds((float) 2.5);
        SceneManager.LoadScene(4);
    }

    bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position) <= reach;
    }
}
