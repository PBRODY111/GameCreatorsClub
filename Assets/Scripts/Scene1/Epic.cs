using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Scene1
{
    public class Epic : MonoBehaviour
    {
        public void Update()
        {
            if(!Player.Player.Instance.EpicModeEnabled()) return;
            //Complete Level
            if (Input.GetKeyDown(KeyCode.F4))
            {
                var vent = FindObjectOfType<Vent>();
                StartCoroutine(vent.EscapeFunc());
            }

            //Jumpscare
            if (Input.GetKeyDown(KeyCode.F5))
            {
                var jumpscare = FindObjectOfType<Jumpscare>();
                StartCoroutine(jumpscare.JumpscareSequence());
            }
        }
    }
}