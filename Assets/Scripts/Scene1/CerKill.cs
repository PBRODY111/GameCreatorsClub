using UnityEngine;
using UnityEngine.SceneManagement;

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
                string sceneName = SceneManager.GetActiveScene().name;
                string sceneNameLowercase = sceneName.ToLower();
                Debug.Log(sceneNameLowercase);
                SaveSystem.SaveHint("cer",sceneNameLowercase);
                StartCoroutine(jumpscare.JumpscareSequence());
            }
        }
    }
}