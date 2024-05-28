using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoublePong : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText;
    [SerializeField] private GameObject dpUI;
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject elements;
    [SerializeField] private GameObject level1;
    [SerializeField] private GameObject level2;
    [SerializeField] private GameObject level3;
    [SerializeField] private GameObject dead;
    [SerializeField] private GameObject overParent;
    [SerializeField] private GameObject inhibitor;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text livesText;
    public int level;
    public int broken;
    public int lives = 5;
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
            ResetGame();
            dpUI.SetActive(false);
            Player.Player.Instance.LockCursor();
            Player.Player.Instance.EnableMovement();
        }
    }

    public void ResetGame(){
        foreach (Transform child in level1.transform){
            child.gameObject.SetActive(true);
        }
        foreach (Transform child in level2.transform){
            child.gameObject.SetActive(true);
        }
        foreach (Transform child in level3.transform){
            child.gameObject.SetActive(true);
        }
        level1.SetActive(true);
        level2.SetActive(false);
        level3.SetActive(false);
        level = 1;
        broken = 19;
        lives = 5;
        levelText.text = "LEVEL: 1";
        livesText.text = "LIVES: 5";
        elements.SetActive(false);
        menu.SetActive(true);
        dpUI.SetActive(false);
        Player.Player.Instance.LockCursor();
        Player.Player.Instance.EnableMovement();
    }

    public void GameWin(){
        ball.SetActive(false);
        dead.SetActive(true);
        Debug.Log("GAME WIN?");
        StartCoroutine(WinSequence());
    }

    IEnumerator WinSequence(){
        Debug.Log("Enters Method?");
        yield return new WaitForSeconds(5f);
        Debug.Log("GAME WIN???");
        dpUI.SetActive(false);
        Player.Player.Instance.LockCursor();
        Player.Player.Instance.EnableMovement();
        inhibitor.SetActive(true);
        overParent.SetActive(false);
        intText.SetActive(false);
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
