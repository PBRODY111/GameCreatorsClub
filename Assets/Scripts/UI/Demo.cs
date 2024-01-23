using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class Demo : MonoBehaviour
    {
        private static readonly int IsProceed = Animator.StringToHash("isProceed");
        [SerializeField] private Animator splashAnim;
        
        
        private void Start()
        {
            QualitySettings.vSyncCount = 1;
        }
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) StartCoroutine(RunSplashScreen());
        }

        private IEnumerator RunSplashScreen()
        {
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}