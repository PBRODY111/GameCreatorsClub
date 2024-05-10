using System.Collections;
using UnityEngine;

namespace Scene6
{
    public class DoorCloseTrigger : MonoBehaviour
    {
        [SerializeField] private Animator doorAnim;
        [SerializeField] private GameObject cerberus;
        [SerializeField] private AudioSource chaseAudio;
        [SerializeField] private AudioSource slamAudio;
        [SerializeField] private AudioSource droneAudio;
        [SerializeField] private Camera playerCam;
        [SerializeField] private Camera cerberusCamera;
        private bool hasEntered = false;
        private static readonly int IsClosed = Animator.StringToHash("isClosed");

        void Start(){
            Debug.Log("STAERT");
        }
    
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.name == "Zagreus" && !hasEntered)
            {
                doorAnim.SetBool(IsClosed, true);
                slamAudio.Play();
                StartCoroutine(CloseDoor());
                Debug.Log("ZAGREUS");
                hasEntered = true;
            }
        }

        IEnumerator CloseDoor(){
            yield return new WaitForSeconds(0.5f);
            playerCam.enabled = true;
            cerberusCamera.enabled = false;
            cerberus.SetActive(false);
            chaseAudio.Stop();
            droneAudio.Play();
        }
    }
}
