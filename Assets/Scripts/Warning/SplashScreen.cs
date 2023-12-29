using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private Animator splashAnim;
    // Start is called before the first frame update
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            StartCoroutine(splashScreen());
        }
    }

    IEnumerator splashScreen()
    {
        splashAnim.SetBool("isProceed", true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
