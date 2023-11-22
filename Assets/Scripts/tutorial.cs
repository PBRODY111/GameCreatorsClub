using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorial : MonoBehaviour
{
    [SerializeField] private GameObject entire;
    [SerializeField] private GameObject page1;
    [SerializeField] private GameObject page2;
    [SerializeField] private GameObject page3;
    [SerializeField] private GameObject ui0;
    [SerializeField] private GameObject ui1;
    [SerializeField] private GameObject ui2;
    [SerializeField] private GameObject ui3;
    [SerializeField] private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        page1.SetActive(true);
    }

    // Update is called once per frame
    public void goPage1()
    {
        page1.SetActive(true);
        page2.SetActive(false);
        page3.SetActive(false);
    }
    public void goPage2()
    {
        page1.SetActive(false);
        page2.SetActive(true);
        page3.SetActive(false);
    }
    public void goPage3()
    {
        page1.SetActive(false);
        page2.SetActive(false);
        page3.SetActive(true);
    }
    public void hide()
    {
        entire.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        animator.SetBool("tutorialClose", true);
        ui0.SetActive(true);
        ui0.GetComponent<Animator>().Play("Room1Preload");
        ui1.SetActive(true);
        ui1.GetComponent<Animator>().Play("Room1Preload");
        ui2.SetActive(true);
        ui2.GetComponent<Animator>().Play("Room1Preload");
        ui3.SetActive(true);
        ui3.GetComponent<Animator>().Play("Room1Preload");
    }

    void Update(){
        if(entire.activeInHierarchy){
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
