using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionBoard : MonoBehaviour
{
    [SerializeField] private string [] set1;
    [SerializeField] private string [] set2;
    [SerializeField] private string [] set3;
    [SerializeField] private string [] set4;
    [SerializeField] private GameObject intText;
    [SerializeField] private GameObject questionUI;
    [SerializeField] private float reach;
    [SerializeField] private TMP_Text textBlock;
    [SerializeField] private int level = 1;
    [SerializeField] private PhoneAudio phoneAudio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseExit()
    {
        intText.SetActive(false);
    }

    private void OnMouseOver()
    {
        intText.SetActive(IsWithinReach() && phoneAudio.emergencyActive);
        if (Input.GetKeyDown(KeyCode.E) && IsWithinReach() && phoneAudio.emergencyActive)
        {
            questionUI.SetActive(true);
            intText.SetActive(false);
            Player.Player.Instance.UnlockCursor();
            Player.Player.Instance.DisableMovement();
            StartCoroutine(AskQuestions());
        }
    }

    public void HideQuestion(){
        questionUI.SetActive(false);
        Player.Player.Instance.LockCursor();
        Player.Player.Instance.EnableMovement();
    }

    IEnumerator AskQuestions(){
        yield return new WaitForSeconds(1);
        if(level == 1){
            textBlock.text = "LEVEL 1";
        }
        yield return new WaitForSeconds(2);
        textBlock.text = "EACH LEVEL CONTAINS 3 QUESTIONS";
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
