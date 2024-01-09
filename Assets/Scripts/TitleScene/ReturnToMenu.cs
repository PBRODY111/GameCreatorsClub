using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SceneManager.LoadScene(1);
    }
}