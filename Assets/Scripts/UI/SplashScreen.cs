using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class SplashScreen : MonoBehaviour
    {
        private static readonly int IsProceed = Animator.StringToHash("isProceed");
        [SerializeField] private Animator splashAnim;
        
        
        private void Start()
        {
            Screen.fullScreen = true;
            QualitySettings.vSyncCount = 1;
        }
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) StartCoroutine(RunSplashScreen());
        }

        private IEnumerator RunSplashScreen()
        {
            splashAnim.SetBool(IsProceed, true);
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}