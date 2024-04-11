using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UI;
using UnityEngine.UI;

public class ImagePanel : MonoBehaviour
{
    [SerializeField] private TextMeshPro signText;
    [SerializeField] private TMP_Text signText2;
    [SerializeField] private bool signBool; // true is multiply
    [SerializeField] private GameObject intText;
    [SerializeField] private GameObject panelUI;
    [SerializeField] private GameObject imageSlot;
    [SerializeField] private GameObject textSlot;
    [SerializeField] private Sprite panelSprite;
    [SerializeField] private int[] answers;
    [SerializeField] private int answer;
    [SerializeField] private float reach;
    [SerializeField] private PhoneAudio phoneAudio;
    // Start is called before the first frame update
    void Start()
    {
        int randomInt = Random.Range(0, 2);
        signBool = (randomInt == 0) ? false : true;
        if(signBool){
            signText.text = "x";
        } else{
            signText.text = "+";
        }
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
            panelUI.SetActive(true);
            imageSlot.SetActive(true);
            textSlot.SetActive(false);
            intText.SetActive(false);
            if(signBool){
                signText2.text = "x";
            } else{
                signText2.text = "+";
            }
            imageSlot.GetComponent<Image>().sprite = panelSprite;
            Player.Player.Instance.UnlockCursor();
            Player.Player.Instance.DisableMovement();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
