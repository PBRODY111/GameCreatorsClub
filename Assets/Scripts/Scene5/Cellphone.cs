using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Steamworks;

public class Cellphone : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText;
    [SerializeField] private GameObject phoneUI;
    [SerializeField] private string code = "2211814";
    [SerializeField] private string _entered = "";
    [SerializeField] private AudioSource dial;
    [SerializeField] private AudioSource calling;
    [SerializeField] private AudioSource message;
    // Start is called before the first frame update
    private void OnMouseExit()
    {
        intText.SetActive(false);
    }

    private void OnMouseOver()
    {
        if(IsWithinReach()){
            intText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && IsWithinReach()){
                phoneUI.SetActive(true);
                PauseMenu.IsPaused = true;
                Player.Player.Instance.UnlockCursor();
            }
        }
    }

    public void AddNumb(Button button)
    {
        dial.Play();
        _entered += button.name;
        if (_entered.Length >= 7)
        {
            if (_entered == code){
                StartCoroutine(CallPhone());
            }

            _entered = "";
            phoneUI.SetActive(false);
            PauseMenu.IsPaused = false;
            Player.Player.Instance.LockCursor();
        }
    }

    IEnumerator CallPhone(){
        calling.Play();
        yield return new WaitForSeconds(8.0f);
        message.Play();
        //STEAM ACHIEVEMENTS
        if(SteamManager.Initialized){
            SteamUserStats.SetAchievement("LOREKEEPER_5");
            SteamUserStats.StoreStats();
        }
    }

    private void Update()
    {
        if (phoneUI.activeSelf)
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                phoneUI.SetActive(false);
                PauseMenu.IsPaused = false;
                Player.Player.Instance.LockCursor();
            }
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
