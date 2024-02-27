using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using Player.Inventory;

public class Vhs : MonoBehaviour
{
    [SerializeField] private AudioClip [] audioList;
    [SerializeField] private AudioSource computerAudio;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private GameObject intText;
    [SerializeField] private GameObject intText3;
    [SerializeField] private GameObject computerUI;
    [SerializeField] private GameObject rawImg;
    [SerializeField] private Hotplate hotPlate;
    [SerializeField] private float reach;
    private bool hasDvd = false;
    private readonly bool _isUnlocked = false;
    // Start is called before the first frame update
    void Start()
    {
        rawImg.SetActive(false);
        Debug.Log(hotPlate.chemicalName);
        Debug.Log(hotPlate.stirSize);
        if(hotPlate.chemicalName == "G Jug"){
            if(hotPlate.stirSize == "L Stir"){
                computerAudio.clip = audioList[0];
            } else{
                computerAudio.clip = audioList[1];
            }
        } else if(hotPlate.chemicalName == "W Jug"){
            if(hotPlate.stirSize == "L Stir"){
                computerAudio.clip = audioList[2];
            } else{
                computerAudio.clip = audioList[3];
            }
        } else{
            if(hotPlate.stirSize == "L Stir"){
                computerAudio.clip = audioList[4];
            } else{
                computerAudio.clip = audioList[5];
            }
        }
    }

    private void OnMouseExit()
    {
        intText.SetActive(false);
        intText3.SetActive(false);
    }

    private void OnMouseOver()
    {
        if(hasDvd){
            if (!computerUI.activeSelf && !_isUnlocked)
                intText.SetActive(IsWithinReach());
            if (Input.GetKeyDown(KeyCode.E) && !_isUnlocked && IsWithinReach())
            {
                intText.SetActive(false);
                computerUI.SetActive(true);
                Player.Player.Instance.UnlockCursor();
                Player.Player.Instance.DisableMovement();
            }
        } else{
            intText3.GetComponent<TMP_Text>().text = "DVD NEEDED TO INTERACT";
            intText3.SetActive(IsWithinReach());

            if (Input.GetMouseButtonDown(1) && IsWithinReach() && Player.Player.Instance.IsHolding("DVD"))
            {
                intText.SetActive(false);
                Player.Player.Instance.hotbar.transform.GetChild(Player.Player.Instance.selectedslot).GetComponent<InventoryItemController>().RemoveItem();
                Player.Player.Instance.selectedslot = -1;
                computerUI.SetActive(true);
                Player.Player.Instance.UnlockCursor();
                Player.Player.Instance.DisableMovement();
            }
        }
    }

    public void ShutDown()
    {
        computerAudio.Pause();
        videoPlayer.Pause();
        rawImg.SetActive(false);
        Player.Player.Instance.LockCursor();
        Player.Player.Instance.EnableMovement();
        computerUI.SetActive(false);
    }
    public void PlayButton()
    {
        rawImg.SetActive(true);
        if(videoPlayer.isPlaying){
            computerAudio.Pause();
            videoPlayer.Pause();
        } else{
            computerAudio.Play();
            videoPlayer.Play();
        }

    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
