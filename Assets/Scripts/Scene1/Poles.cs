using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poles : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText;
    [SerializeField] private GameObject ladderUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseOver()
    {
        intText.SetActive(IsWithinReach());
        if (Input.GetKeyDown(KeyCode.E) && IsWithinReach())
        {
            ladderUI.SetActive(true);
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
