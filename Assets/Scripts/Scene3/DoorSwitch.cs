using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    [SerializeField] private Keyhole2 keyhole;
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private AudioSource switchAudio;
    [SerializeField] private AudioSource doorAudio;
    [SerializeField] private Animator doorAnim;
    private static readonly int IsOpen = Animator.StringToHash("isOpen");
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseExit()
    {
        intText.SetActive(false);
    }

    private void OnMouseOver()
    {
        intText.SetActive(IsWithinReach());
        if (Input.GetKeyDown(KeyCode.E) && IsWithinReach())
        {
            if(keyhole.hasKey){
                transform.rotation = Quaternion.Euler(-135, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                switchAudio.clip = clips[0];
                switchAudio.Play();
                doorAudio.Play();
                doorAnim.SetBool(IsOpen, true);
            } else{
                switchAudio.clip = clips[1];
                switchAudio.Play();
            }
        }
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
