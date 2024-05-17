using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Player.Inventory;

public class Sentry : MonoBehaviour
{
    [SerializeField] private Camera playerCam;
    [SerializeField] private Camera sentryCam;
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText3;
    [SerializeField] private GameObject axe;
    [SerializeField] private Animator sentryAnim;
    [SerializeField] private AudioSource sentryAudio;
    [SerializeField] private AudioSource jumpscareAudio;
    public bool canInteract = false;
    private static readonly int IsActive = Animator.StringToHash("isActive");
    private static readonly int IsGrab = Animator.StringToHash("isGrab");
    private static readonly int IsJumpscare = Animator.StringToHash("isJumpscare");
    [SerializeField] private string item = "";
    [SerializeField] private string [] itemList;
    private int itemIndex = 0;
    
    private void Start(){
        playerCam.enabled = true;
        sentryCam.enabled = false;
        item = itemList[itemIndex];
    }

    private void OnMouseExit()
    {
        intText3.SetActive(false);
    }

    private void OnMouseOver()
    {
        if(IsWithinReach() && canInteract){
            intText3.GetComponent<TMP_Text>().text = item.ToUpper()+" NEEDED TO INTERACT";
            intText3.SetActive(true);

            if (Input.GetMouseButtonDown(1) && IsWithinReach()){
                if(Player.Player.Instance.IsHolding(item)){
                    // something
                    Player.Player.Instance.hotbar.transform.GetChild(Player.Player.Instance.selectedslot).GetComponent<InventoryItemController>().RemoveItem();
                    Player.Player.Instance.selectedslot = -1;
                    StartCoroutine(NextItem());
                } else{
                    // jumpscare the shit out of 'em
                    canInteract = false;
                    StartCoroutine(JumpscareSequence());
                }
            }
        }
    }

    public IEnumerator NextItem(){
        intText3.SetActive(false);
        canInteract = false;
        sentryAnim.SetBool(IsGrab, true);
        sentryAudio.Play();
        yield return new WaitForSeconds(2f);
        sentryAnim.SetBool(IsGrab, false);
        sentryAudio.Play();
        yield return new WaitForSeconds(1f);
        if(itemIndex < itemList.Length-1){
            itemIndex++;
            item = itemList[itemIndex];
            canInteract = true;
        } else{
            sentryAudio.Play();
            sentryAnim.SetBool(IsActive, false);
            axe.SetActive(true);
        }
    }

    public IEnumerator JumpscareSequence(){
        SaveSystem.SaveHint("paradox","room5");
        Debug.Log("KILL!!");
        sentryCam.enabled = true;
        sentryAnim.SetBool(IsJumpscare, true);
        jumpscareAudio.Play();
        yield return new WaitForSeconds(1.9f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("GameOverScene");
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
