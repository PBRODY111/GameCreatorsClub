using System.Collections;
using UnityEngine;

namespace Scene6
{
    public class DoorCloseTrigger1 : MonoBehaviour
    {
        [SerializeField] private Animator doorAnim;
        [SerializeField] private AudioSource slamAudio;
        [SerializeField] private LightFlicker lightFlicker;
        private bool hasEntered = false;
        private static readonly int IsClosed = Animator.StringToHash("isClosed");

        void Start(){
            Debug.Log("STAERT");
        }
    
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.name == "Myra4" && !hasEntered)
            {
                doorAnim.SetBool(IsClosed, true);
                slamAudio.Play();
                hasEntered = true;
                lightFlicker.isActive = true;
                StartCoroutine(lightFlicker.FlickerLight());
            }
        }
    }
}
