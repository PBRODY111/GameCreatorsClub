using System.Collections;
using UnityEngine;

namespace Scene1
{
    public class ClosetDoor : MonoBehaviour
    {
        [SerializeField] private float reach;
        [SerializeField] private GameObject intText;
        [SerializeField] private GameObject scare;
        [SerializeField] private AudioSource doorAudio;
        private Animator _doorAnim;
        private int _probInt;
        private static readonly int IsOpen = Animator.StringToHash("isOpen");

        private void Awake()
        {
            _doorAnim = GetComponent<Animator>();
            if (_doorAnim == null)
                _doorAnim = GetComponentInChildren<Animator>();
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
                doorAudio.pitch *= -1;
                doorAudio.timeSamples = doorAudio.pitch > 0 ? 0 : doorAudio.clip.samples - 1;
                doorAudio.Play(0);
                _doorAnim.SetBool(IsOpen, !_doorAnim.GetBool(IsOpen));
                _probInt = Random.Range(0, 6);
                if (_probInt == 1) StartCoroutine(ImgScare());
            }
        }

        private IEnumerator ImgScare()
        {
            _probInt = Random.Range(0, 6);
            scare.SetActive(true);
            scare.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(1.5f);
            scare.SetActive(false);
        }

        private bool IsWithinReach()
        {
            return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
        }
    }
}