using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Drawer : MonoBehaviour
{
    [SerializeField] private GameObject intText3;
    [SerializeField] private GameObject scare;
    [SerializeField] private float reach;
    [SerializeField] private AudioSource closetAudio;
    private Animator _drawerAnim;
    private int probInt;
    
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
        if(Input.GetMouseButtonDown(1) && IsWithinReach() && InventoryItemController.item.itemName == "Lock Pick"){
            closetAudio.pitch *= -1;
            closetAudio.timeSamples = closetAudio.pitch > 0 ? 0 : closetAudio.clip.samples - 1;
            closetAudio.Play(0);
            _drawerAnim.SetBool("isOpen", !_drawerAnim.GetBool("isOpen"));
            probInt = Random.Range(0, 5);
            if(probInt == 1){
                StartCoroutine(imgScare());
            }
        }
    }
    void OnMouseExit()
    {
        intText3.SetActive(false);
    }

    IEnumerator imgScare(){
        scare.SetActive(true);
        scare.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1.5f);
        scare.SetActive(false);
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
