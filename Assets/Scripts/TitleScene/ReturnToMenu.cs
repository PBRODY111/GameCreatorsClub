using UnityEngine;
using UnityEngine.SceneManagement;

namespace TitleScene
{
    public class ReturnToMenu : MonoBehaviour
    {
        [SerializeField] private int chance;
        void Start(){
            Cursor.lockState = CursorLockMode.None;
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0)){
                chance = Random.Range(0, 2);
                Cursor.lockState = CursorLockMode.None;
                if(chance == 0){
                    SceneManager.LoadScene("Styx");
                } else{ 
                    SceneManager.LoadScene("WarningScene");
                }
            }
        }
    }
}