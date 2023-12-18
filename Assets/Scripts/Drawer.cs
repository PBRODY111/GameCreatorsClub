using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Drawer : MonoBehaviour
{
    [SerializeField] private GameObject intText3;
    [SerializeField] private float reach;
    [SerializeField] private AudioSource closetAudio;
    private Animator _drawerAnim;
    
    // Start is called before the first frame update
    void Awake(){
        _drawerAnim = GetComponent<Animator>();
        if(_drawerAnim == null)
            _drawerAnim = GetComponentInChildren<Animator>();
    }
    void OnMouseOver()
    {
        intText3.GetComponent<TMP_Text>().text = "LOCKPICK NEEDED TO INTERACT";
        intText3.SetActive(IsWithinReach());
        if(Input.GetMouseButtonDown(1) && IsWithinReach()){
            closetAudio.pitch *= -1;
            closetAudio.timeSamples = closetAudio.pitch > 0 ? 0 : closetAudio.clip.samples - 1;
            closetAudio.Play(0);
            _drawerAnim.SetBool("isOpen", !_drawerAnim.GetBool("isOpen"));
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

    bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position) <= reach;
    }
}
