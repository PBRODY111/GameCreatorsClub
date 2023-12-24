using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText;
    [SerializeField] private GameObject ventUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseOver()
    {
        intText.SetActive(IsWithinReach());
        if (Input.GetKeyDown(KeyCode.E) && IsWithinReach())
        {
            ventUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            PauseMenu.isPaused = true;
        }
    }
    void OnMouseExit()
    {
        intText.SetActive(false);
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
    }

    bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position) <= reach;
    }
}
