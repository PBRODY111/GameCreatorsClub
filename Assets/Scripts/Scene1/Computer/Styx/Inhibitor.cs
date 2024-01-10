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

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
                // THIS WILL GIVE AN EXTRA STAR ENDING (MAX OF 2)
                SceneManager.LoadScene("TitleScene");
        }
    }
}