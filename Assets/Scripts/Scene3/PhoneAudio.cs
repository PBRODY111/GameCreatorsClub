using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneAudio : MonoBehaviour
{
    [SerializeField] private GameObject intText;
    [SerializeField] private float reach;
    [SerializeField] private AudioClip[] audios;
    [SerializeField] private AudioSource phoneAudio;
    [SerializeField] private Attack3Cer cerAttack;
    [SerializeField] private GameObject[] objectsToHide;
    [SerializeField] private GameObject sparks;
    public bool emergencyActive = false;
    private Animator _phoneAnim;
    private static readonly int IsFall = Animator.StringToHash("isFall");
    // Start is called before the first frame update
    private void Awake()
    {
        _phoneAnim = GetComponent<Animator>();
        if (_phoneAnim == null)
            _phoneAnim = GetComponentInChildren<Animator>();
        foreach (GameObject obj in objectsToHide)
        {
            obj.SetActive(false);
        }
        sparks.SetActive(false);
    }

    private void OnMouseExit()
    {
        intText.SetActive(false);
    }

    private void OnMouseOver()
    {
        if(!_phoneAnim.GetBool(IsFall)){
            intText.SetActive(IsWithinReach());
            if (Input.GetKeyDown(KeyCode.E) && IsWithinReach())
            {
                StartCoroutine(PhoneFall());
            }
        }
    }

    IEnumerator PhoneFall(){
        phoneAudio.clip = audios[0];
        phoneAudio.Play();
        yield return new WaitForSeconds(1);
        phoneAudio.clip = audios[1];
        phoneAudio.Play();
        yield return new WaitForSeconds(8);
        phoneAudio.clip = audios[2];
        phoneAudio.Play();
        _phoneAnim.SetBool(IsFall, true);
        cerAttack.attacking = true;
        emergencyActive = true;
        foreach (GameObject obj in objectsToHide)
        {
            obj.SetActive(true);
        }
        sparks.SetActive(true);
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
