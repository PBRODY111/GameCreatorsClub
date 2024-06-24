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
    [SerializeField] private GameObject spider;
    [SerializeField] private GameObject bat;
    [SerializeField] private GameObject troll;
    [SerializeField] private GameObject amalg;
    [SerializeField] private GameObject hand;
    [SerializeField] private GameObject inhibitor;
    [SerializeField] private GameObject cabinet;
    [SerializeField] private AudioSource subft;
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
            Reset();
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
            if(level == 2){
                spider.SetActive(true);
            } else if (level == 3){
                bat.SetActive(true);
            } else if (level == 4){
                bat.SetActive(false);
                spider.SetActive(false);
                troll.SetActive(false);
                amalg.SetActive(true);
                amalg.transform.localPosition = new Vector3(640f, -310f, 0f);
            } else if(level == 5){
                bat.SetActive(false);
                spider.SetActive(false);
                troll.SetActive(false);
                amalg.SetActive(false);
                gameElements.SetActive(false);
                subft.Stop();
                hand.SetActive(true);
                StartCoroutine(EndGame());
            }
        }
    }

    public void Reset(){
        level = 1;
        points = 0;
        pointsText.text = "0/15";
        levelText.text = "LEVEL 1";
        DestroyChildren("Bullets");
        DestroyChildren("Spider");
        DestroyChildren("Troll");
        DestroyChildren("Bat");
        DestroyChildren("FastBat");
        DestroyChildren("FastSpider");
        amalg.transform.localPosition = new Vector3(640f, -310f, 0f);
        troll.SetActive(false);
        spider.SetActive(false);
        bat.SetActive(false);
        amalg.SetActive(false);
        gameElements.SetActive(false);
        menu.SetActive(true);
        dashrUI.SetActive(false);
        Player.Player.Instance.LockCursor();
        Player.Player.Instance.EnableMovement();
    }

    private void DestroyChildren(string targetObjectName)
    {
        GameObject parentObject = GameObject.Find(targetObjectName);
        if (parentObject != null)
        {
            // Iterate through all children and destroy them
            foreach (Transform child in parentObject.transform)
            {
                Destroy(child.gameObject);
            }

            Debug.Log("All children of " + targetObjectName + " have been destroyed.");
        }
        else
        {
            Debug.LogWarning("GameObject with name " + targetObjectName + " not found.");
        }
    }

    IEnumerator EndGame(){
        yield return new WaitForSeconds(3f);
        inhibitor.SetActive(true);
        yield return new WaitForSeconds(2f);
        Reset();
        cabinet.SetActive(false);
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
