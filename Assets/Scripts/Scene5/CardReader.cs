using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardReader : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText3;
    [SerializeField] private GameObject red;
    [SerializeField] private Animator doorAnim;
    [SerializeField] private Animator sentryAnim;
    [SerializeField] private AudioSource unlockAudio;
    [SerializeField] private AudioSource sentryAudio;
    [SerializeField] private Sentry sentry;
    private static readonly int IsOpen = Animator.StringToHash("isOpen");
    private static readonly int IsActive = Animator.StringToHash("isActive");
    private bool isIn = false;
    // Start is called before the first frame update
    private void OnMouseExit()
    {
        intText3.SetActive(false);
    }

    private void OnMouseOver()
    {
        if(IsWithinReach() && !isIn){
            intText3.GetComponent<TMP_Text>().text = "KEYCARD NEEDED TO INTERACT";
            intText3.SetActive(true);

            if (Input.GetMouseButtonDown(1) && IsWithinReach() && Player.Player.Instance.IsHolding("Keycard")){
                isIn = true;
                doorAnim.SetBool(IsOpen, true);
                intText3.SetActive(false);
                StartCoroutine(SentryActive());
                unlockAudio.Play();
                red.SetActive(false);
            }
        }
    }

    IEnumerator SentryActive(){
        yield return new WaitForSeconds(3f);
        sentryAudio.Play();
        sentryAnim.SetBool(IsActive, true);
        sentry.canInteract = true;
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
