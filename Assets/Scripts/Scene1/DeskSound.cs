using UnityEngine;

namespace Scene1
{
    public class DeskSound : MonoBehaviour
    {
        [SerializeField] private AudioSource impactAudio;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.relativeVelocity.magnitude > 3f) impactAudio.Play();
        }
    }
}