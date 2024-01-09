using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private Animator splashAnim;
    private static readonly int IsProceed = Animator.StringToHash("isProceed");

    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(RunSplashScreen());
        }
    }

    private IEnumerator RunSplashScreen()
    {
        splashAnim.SetBool(IsProceed, true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}