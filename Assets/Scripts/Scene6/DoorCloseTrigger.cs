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
        private static readonly int IsClosed = Animator.StringToHash("isClosed");

        void Start(){
            Debug.Log("STAERT");
        }
    
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.name == "Zagreus")
            {
                doorAnim.SetBool(IsClosed, true);
                slamAudio.Play();
                StartCoroutine(CloseDoor());
                Debug.Log("ZAGREUS");
            }
        }

        IEnumerator CloseDoor(){
            yield return new WaitForSeconds(0.5f);
            cerberus.SetActive(false);
            chaseAudio.Stop();
            droneAudio.Play();
        }
    }
}
