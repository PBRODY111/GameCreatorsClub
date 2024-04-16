using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corpse : MonoBehaviour
{
    [SerializeField] private GameObject myra;
    [SerializeField] private AudioSource shriek;
    private Animator _anim;
    private static readonly int IsJumpscare = Animator.StringToHash("isJumpscare");
    [SerializeField] private Room3Escape escapeFunc;

    private void Awake()
    {
        myra.SetActive(false);
        _anim = GetComponent<Animator>();
        if (_anim == null)
            _anim = GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Zagreus")
        {
            StartCoroutine(Jumpscare());
        }
    }
    IEnumerator Jumpscare(){
        _anim.SetBool(IsJumpscare, true);
        shriek.Play();
        yield return new WaitForSeconds(0.5f);
        myra.SetActive(true);
        escapeFunc.StartEnd();
        gameObject.SetActive(false);
    }
}
