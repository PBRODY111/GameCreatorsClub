using UnityEngine;

namespace Scene1
{
    public class CerKill : MonoBehaviour
    {
        public Jumpscare jumpscare;
        public bool stopAttack = false;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.name == "Zagreus")
            {
                stopAttack = true;
                StartCoroutine(jumpscare.JumpscareSequence());
            }
        }
    }
}