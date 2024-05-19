using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Player.Inventory;

public class Lock : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText3;
    [SerializeField] private GameObject intText;
    [SerializeField] private GameObject bigLockUI;
    [SerializeField] private GameObject key;
    [SerializeField] private string code;
    [SerializeField] private string _entered = "";
    [SerializeField] private TextMeshPro t1;
    [SerializeField] private TextMeshPro t2;
    [SerializeField] private TextMeshPro t3;
    [SerializeField] private TextMeshPro t4;
    [SerializeField] private AudioSource dial;
    [SerializeField] private AudioSource unlock;
    [SerializeField] private Animator lDoorAnim;
    [SerializeField] private Animator rDoorAnim;
    private static readonly int IsOpen = Animator.StringToHash("isOpen");
    private bool hasKey = false;
    // Start is called before the first frame update
    private void Start()
    {
        for (var i = 0; i < 4; i++) code += Random.Range(1, 5);
        t1.text = ""+code[0];
        t2.text = ""+code[1];
        t3.text = ""+code[2];
        t4.text = ""+code[3];
    }

    private void OnMouseExit()
    {
        intText3.SetActive(false);
        intText.SetActive(false);
    }

    private void OnMouseOver()
    {
        if(IsWithinReach() && !hasKey){
            intText3.GetComponent<TMP_Text>().text = "BIG KEY NEEDED TO INTERACT";
            intText3.SetActive(true);
            if (Input.GetMouseButtonDown(1) && IsWithinReach()){
                if(Player.Player.Instance.IsHolding("Big Key")){
                    intText3.SetActive(false);
                    Inventory.Instance.RemoveSelectedItem();
                    key.SetActive(true);
                    hasKey = true;
                }
            }
        } else if(IsWithinReach() && hasKey){
            intText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && IsWithinReach()){
                bigLockUI.SetActive(true);
                PauseMenu.IsPaused = true;
                Player.Player.Instance.UnlockCursor();
            }
        }
    }

    public void AddNumb(Button button)
    {
        dial.Play();
        _entered += button.name;
        if (_entered.Length >= 4)
        {
            if (_entered == code){
                lDoorAnim.SetBool(IsOpen, true);
                rDoorAnim.SetBool(IsOpen, true);
                unlock.Play();
                bigLockUI.SetActive(false);
                PauseMenu.IsPaused = false;
                Player.Player.Instance.LockCursor();
                gameObject.SetActive(false);
            }

            _entered = "";
            bigLockUI.SetActive(false);
            PauseMenu.IsPaused = false;
            Player.Player.Instance.LockCursor();
        }
    }

    private void Update()
    {
        if (bigLockUI.activeSelf)
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                bigLockUI.SetActive(false);
                PauseMenu.IsPaused = false;
                Player.Player.Instance.LockCursor();
            }
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
