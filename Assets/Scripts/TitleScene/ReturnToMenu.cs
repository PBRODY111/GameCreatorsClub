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
                SaveData data = SaveSystem.LoadMinigame();
                chance = Random.Range(0, 3);
                Cursor.lockState = CursorLockMode.None;
                if(data != null){
                    if(data.styx){
                        SceneManager.LoadScene("WarningScene");
                    } else{
                        if(chance == 0){
                            SceneManager.LoadScene("Styx");
                        } else{ 
                            SceneManager.LoadScene("WarningScene");
                        }
                    }
                } else{
                    if(chance == 0){
                        SceneManager.LoadScene("Styx");
                    } else{ 
                        SceneManager.LoadScene("WarningScene");
                    }
                }
            }
        }
    }
}