using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Vent : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText;
    [SerializeField] private GameObject intText3;
    [SerializeField] private GameObject ventUI;
    public int unscrewed = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseOver()
    {
        if(unscrewed < 4){
            intText.SetActive(IsWithinReach());
            if (Input.GetKeyDown(KeyCode.E) && IsWithinReach())
            {
                ventUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                PauseMenu.isPaused = true;
            }
        } else{
            intText3.GetComponent<TMP_Text>().text = "CROWBAR NEEDED TO INTERACT";
            intText3.SetActive(true);
        }
    }
    void OnMouseExit()
    {
        intText.SetActive(false);
        intText3.SetActive(false);
    }

    void Update()
    {
        if (ventUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ventUI.SetActive(false);
                PauseMenu.isPaused = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        if (unscrewed >= 4){
            ventUI.SetActive(false);
        }
    }

    bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position) <= reach;
    }
}
