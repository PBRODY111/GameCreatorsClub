using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiveraLever : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText;
    [SerializeField] private AudioSource switchAudio;
    [SerializeField] private RiveraLever lev1;
    [SerializeField] private RiveraLever lev2;
    public bool directionDown = true;
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
        intText.SetActive(IsWithinReach());
        if (Input.GetKeyDown(KeyCode.E) && IsWithinReach())
        {
            SwitchDirection();
            lev1.SwitchDirection();
            lev2.SwitchDirection();
        }
    }

    public void SwitchDirection(){
        int currentRotationX = Mathf.RoundToInt(transform.rotation.eulerAngles.x);
        switchAudio.Play();
        directionDown = !directionDown;
        if(currentRotationX == -150){
            transform.rotation = Quaternion.Euler(-30f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        } else{
            transform.rotation = Quaternion.Euler(-150f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
