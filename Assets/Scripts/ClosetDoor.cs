using UnityEngine;

public class ClosetDoor : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText;
    [SerializeField] private AudioSource doorAudio;
    private Animator _doorAnim;

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
        }
    }

    void OnMouseExit()
    {
        intText.SetActive(false);
    }

    bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position) <= reach;
    }
}
