using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeDoor5 : MonoBehaviour
{
    [SerializeField] private Keyhole redKey;
    [SerializeField] private Keyhole greenKey;
    [SerializeField] private Keyhole blueKey;
    [SerializeField] private AudioSource clickAudio;
    private Animator _doorAnim;
    private static readonly int IsOpen = Animator.StringToHash("isOpen");

    private void Awake()
    {
        _doorAnim = GetComponent<Animator>();
        if (_doorAnim == null)
            _doorAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    public void CheckUnlock()
    {
        if(redKey.hasKey == greenKey.hasKey && greenKey.hasKey == blueKey.hasKey && redKey.hasKey == true){
            _doorAnim.SetBool(IsOpen, true);
            clickAudio.Play();
        }
    }
}
