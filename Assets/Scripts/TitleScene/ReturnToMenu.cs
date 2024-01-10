using UnityEngine;
using UnityEngine.SceneManagement;

namespace TitleScene
{
    public class ReturnToMenu : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) SceneManager.LoadScene("WarningScene");
        }
    }
}