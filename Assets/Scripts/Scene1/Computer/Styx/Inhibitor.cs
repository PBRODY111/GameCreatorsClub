using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene1.Computer.Styx
{
    public class Inhibitor : MonoBehaviour
    {
        public GameObject intText;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && intText != null)
                intText.SetActive(true);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player") && intText != null)
                intText.SetActive(false);
        }

        void Update()
        {
            if (intText.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    SceneManager.LoadScene("TitleScene");
                }
            }
        }
    }
}