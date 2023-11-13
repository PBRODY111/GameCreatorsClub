using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreCutscene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Canvas").transform.GetChild(10).gameObject.SetActive(false);
        StartCoroutine(cutScene());
    }

    IEnumerator cutScene()
    {
        yield return new WaitForSeconds((float) 6.0);
        GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(false);
        yield return new WaitForSeconds((float) 6.0);
        GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(false);
        yield return new WaitForSeconds((float) 4.0);
        GameObject.Find("Canvas").transform.GetChild(4).gameObject.SetActive(false);
        yield return new WaitForSeconds((float) 4.0);
        GameObject.Find("Canvas").transform.GetChild(5).gameObject.SetActive(false);
        yield return new WaitForSeconds((float) 6.0);
        GameObject.Find("Canvas").transform.GetChild(6).gameObject.SetActive(false);
        yield return new WaitForSeconds((float) 4.0);
        GameObject.Find("Canvas").transform.GetChild(7).gameObject.SetActive(false);
        yield return new WaitForSeconds((float) 4.0);
        GameObject.Find("Canvas").transform.GetChild(8).gameObject.SetActive(false);
        yield return new WaitForSeconds((float) 3.5);
        GameObject.Find("Canvas").transform.GetChild(10).gameObject.SetActive(true);
        yield return new WaitForSeconds((float) 0.5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
