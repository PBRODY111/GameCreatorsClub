using UnityEngine;

namespace Scene1
{
    public class CerKill : MonoBehaviour
    {
        public Jumpscare jumpscare;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.name == "Zagreus")
            {
                Debug.Log("Kill!");
                StartCoroutine(jumpscare.JumpscareSequence());
            }
        }
    }
}