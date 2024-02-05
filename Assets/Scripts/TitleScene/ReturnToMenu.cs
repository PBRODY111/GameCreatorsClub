using UnityEngine;
using UnityEngine.SceneManagement;

namespace TitleScene
{
    public class ReturnToMenu : MonoBehaviour
    {
        void Start(){
            Cursor.lockState = CursorLockMode.None;
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0)){
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("WarningScene");
            }
        }
    }
}