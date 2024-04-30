using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RiveraUI : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText;
    [SerializeField] private GameObject riveraUI;
    [SerializeField] private GameObject latch;
    [SerializeField] private Animator doorAnim;
    [SerializeField] private Computer4 comp4;
    [SerializeField] private TMP_InputField textBlock;
    [SerializeField] private AudioSource unlockAudio;
    private static readonly int IsOpen = Animator.StringToHash("isOpen");
    private bool hasUnlocked = false;
    // Start is called before the first frame update
    public void ButtonPressed(){
        if(textBlock.text == comp4.code){
            Player.Player.Instance.LockCursor();
            Player.Player.Instance.EnableMovement();
            riveraUI.SetActive(false);
            latch.SetActive(false);
            unlockAudio.Play();
            doorAnim.SetBool(IsOpen, true);
            hasUnlocked = true;
        } else{
            Player.Player.Instance.LockCursor();
            Player.Player.Instance.EnableMovement();
            riveraUI.SetActive(false);
        }
    }

    private void OnMouseExit()
    {
        intText.SetActive(false);
    }

    private void OnMouseOver()
    {
        intText.SetActive(IsWithinReach() && !hasUnlocked);
        if (Input.GetKeyDown(KeyCode.E) && IsWithinReach() && !hasUnlocked)
        {
            riveraUI.SetActive(true);
            Player.Player.Instance.UnlockCursor();
            Player.Player.Instance.DisableMovement();
        }
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
