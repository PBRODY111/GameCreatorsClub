using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublePong : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText;
    [SerializeField] private GameObject dpUI;
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject elements;
    public int level;
    public int broken;
    public int lives = 3;
    // Start is called before the first frame update
    void Start(){
        level = 1;
        broken = 19;
    }

    private void OnMouseExit()
    {
        intText.SetActive(false);
    }

    private void OnMouseOver()
    {
        intText.SetActive(IsWithinReach());
        if (Input.GetKeyDown(KeyCode.E) && IsWithinReach())
        {
            dpUI.SetActive(true);
            Player.Player.Instance.UnlockCursor();
            Player.Player.Instance.DisableMovement();
        }
    }

    // Update is called once per frame
    private void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            elements.SetActive(false);
            menu.SetActive(true);
            dpUI.SetActive(false);
            Player.Player.Instance.LockCursor();
            Player.Player.Instance.EnableMovement();
        }
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
