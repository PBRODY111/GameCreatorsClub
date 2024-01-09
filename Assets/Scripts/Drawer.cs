using System.Collections;
using UnityEngine;
using TMPro;

public class Drawer : MonoBehaviour
{
    [SerializeField] private GameObject intText3;
    [SerializeField] private GameObject scare;
    [SerializeField] private float reach;
    [SerializeField] private AudioSource closetAudio;
    private Animator _drawerAnim;
    private int _probInt;
    
    private static readonly int IsOpen = Animator.StringToHash("isOpen");

    // Start is called before the first frame update
    private void Awake()
    {
        _drawerAnim = GetComponent<Animator>();
        if (_drawerAnim == null)
            _drawerAnim = GetComponentInChildren<Animator>();
    }

    private void OnMouseOver()
    {
        intText3.GetComponent<TMP_Text>().text = "LOCKPICK NEEDED TO INTERACT";
        intText3.SetActive(IsWithinReach());
        if (Input.GetMouseButtonDown(1) && IsWithinReach() && Player.Instance.GetHeldItem().itemName == "Lock Pick")
        {
            var pitch = closetAudio.pitch;
            pitch = -pitch;
            closetAudio.pitch = pitch;
            closetAudio.timeSamples = pitch > 0 ? 0 : closetAudio.clip.samples - 1;
            closetAudio.Play(0);
            _drawerAnim.SetBool(IsOpen, !_drawerAnim.GetBool(IsOpen));
            _probInt = Random.Range(0, 5);
            if (_probInt == 1)
            {
                StartCoroutine(ImgScare());
            }
        }
    }

    private void OnMouseExit()
    {
        intText3.SetActive(false);
    }

    private IEnumerator ImgScare()
    {
        scare.SetActive(true);
        scare.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1.5f);
        scare.SetActive(false);
    }
    
    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position) <= reach;
    }
}