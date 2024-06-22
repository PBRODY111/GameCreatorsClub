using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DashR : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText;
    [SerializeField] private GameObject dashrUI;
    [SerializeField] private GameObject gameElements;
    [SerializeField] private GameObject menu;
    public int level = 1;
    public int points = 0;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text pointsText;
    
    private void OnMouseExit()
    {
        intText.SetActive(false);
    }

    private void OnMouseOver()
    {
        intText.SetActive(IsWithinReach());
        if (Input.GetKeyDown(KeyCode.E) && IsWithinReach())
        {
            dashrUI.SetActive(true);
            Player.Player.Instance.UnlockCursor();
            Player.Player.Instance.DisableMovement();
        }
    }

    // Update is called once per frame
    private void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            gameElements.SetActive(false);
            menu.SetActive(true);
            dashrUI.SetActive(false);
            Player.Player.Instance.LockCursor();
            Player.Player.Instance.EnableMovement();
        }
    }

    public void IncreasePoint(){
        points++;
        pointsText.text = ""+points+"/15";
        if(points >= 15){
            points = 0;
            pointsText.text = ""+points+"/15";
            level++;
            levelText.text = "LEVEL "+level;
        }
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
