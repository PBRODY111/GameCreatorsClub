using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private GameObject page1;
    private GameObject page2;
    private GameObject page3;
    private GameObject page4;
    [SerializeField] private GameObject UI;

    void Start()
    {
        UI.SetActive(false);
        page1 = transform.GetChild(0).gameObject;
        page2 = transform.GetChild(1).gameObject;
        page3 = transform.GetChild(2).gameObject;
        page4 = transform.GetChild(3).gameObject;
        this.gameObject.SetActive(true);
        page1.SetActive(true);

        page1.GetComponentInChildren<Button>().onClick.AddListener(goPage2);
        page2.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(goPage1);
        page2.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(goPage3);
        page3.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(goPage2);
        page3.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(goPage4);
        page4.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(goPage3);
        page4.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(hide);
        Player.Instance.mainCamera.GetComponent<PlayerCam>().enabled = false;
        Cursor.lockState = CursorLockMode.None;

    }

    public void goPage1()
    {
        page1.SetActive(true);
        page2.SetActive(false);
        page3.SetActive(false);
        page4.SetActive(false);
    }
    public void goPage2()
    {
        page1.SetActive(false);
        page2.SetActive(true);
        page3.SetActive(false);
        page4.SetActive(false);
    }
    public void goPage3()
    {
        page1.SetActive(false);
        page2.SetActive(false);
        page3.SetActive(true);
        page4.SetActive(false);
    }
    public void goPage4()
    {
        page1.SetActive(false);
        page2.SetActive(false);
        page3.SetActive(false);
        page4.SetActive(true);
    }
    public void hide()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
        Player.Instance.mainCamera.GetComponent<PlayerCam>().enabled = true;
        UI.SetActive(true);
    }

}
