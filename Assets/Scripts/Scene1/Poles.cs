using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Poles : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText3;
    [SerializeField] private GameObject ladderUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseOver()
    {
        intText3.GetComponent<TMP_Text>().text = "POLES NEEDED TO INTERACT";
        if(!ladderUI.activeSelf){
            intText3.SetActive(IsWithinReach());
        } else{
            intText3.SetActive(false);
        }
        if (Input.GetMouseButtonDown(1) && IsWithinReach() && Player.Instance.GetHeldItem().itemName == "Poles")
        {
            ladderUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            PauseMenu.isPaused = true;
        }
    }
    void OnMouseExit()
    {
        intText3.SetActive(false);
    }

    void Update()
    {
        if (ladderUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ladderUI.SetActive(false);
                PauseMenu.isPaused = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position) <= reach;
    }
}
