using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SafeDoor3_1 : MonoBehaviour
{
    [SerializeField] private GameObject intText3;
    [SerializeField] private float reach;
    [SerializeField] private AudioSource unlockAudio;
    private Animator safeAnimator;
    public bool isUnlocked = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        safeAnimator = GetComponentInChildren<Animator>();
    }

    void OnMouseOver()
    {
        intText3.GetComponent<TMP_Text>().text = "LOCKPICK NEEDED TO INTERACT";
        if(isUnlocked == false){
            intText3.SetActive(IsWithinReach());
        } else{
            intText3.SetActive(false);
        }
        if(Input.GetMouseButtonDown(1) && IsWithinReach()){
            safeAnimator.SetBool("unlock", true);
            unlockAudio.Play();
            isUnlocked = true;
            intText3.SetActive(false);
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
