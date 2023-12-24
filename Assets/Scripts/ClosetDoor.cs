using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetDoor : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText;
    [SerializeField] private GameObject scare;
    [SerializeField] private AudioSource doorAudio;
    private Animator _doorAnim;
    private int probInt;

    void Awake(){
        _doorAnim = GetComponent<Animator>();
        if(_doorAnim == null)
            _doorAnim = GetComponentInChildren<Animator>();
    }
    void OnMouseOver()
    {
        intText.SetActive(IsWithinReach());
        if (Input.GetKeyDown(KeyCode.E) && IsWithinReach())
        {
            doorAudio.pitch *= -1;
            doorAudio.timeSamples = doorAudio.pitch > 0 ? 0 : doorAudio.clip.samples - 1;
            doorAudio.Play(0);
            _doorAnim.SetBool("isOpen", !_doorAnim.GetBool("isOpen"));
            probInt = Random.Range(0, 9);
            if(probInt == 1){
                StartCoroutine(imgScare());
            }
        }
    }

    void OnMouseExit()
    {
        intText.SetActive(false);
    }

    IEnumerator imgScare(){
        scare.SetActive(true);
        scare.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1.5f);
        scare.SetActive(false);
    }

    bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position) <= reach;
    }
}
