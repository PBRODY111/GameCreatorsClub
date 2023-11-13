using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(splashScreen());
    }

    IEnumerator splashScreen()
    {
        yield return new WaitForSeconds((float) 5.0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
