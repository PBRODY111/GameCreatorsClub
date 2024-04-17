using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakerSwitch : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText;
    [SerializeField] private AudioSource clickAudio;
    public bool switchDirection;
    public bool currDirection = false;
    // Start is called before the first frame update
    void Start(){
        int randomInt = Random.Range(0, 2);
        switchDirection = (randomInt == 0) ? false : true;
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
            clickAudio.Play();
            int currentRotationZ = Mathf.RoundToInt(transform.rotation.eulerAngles.z);
        
            if (currentRotationZ == 180)
            {
                // Set rotation to 0 degrees if it's currently at 180 degrees
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
                currDirection = false;
            }
            else if (currentRotationZ == 0)
            {
                // Set rotation to 180 degrees if it's currently at 0 degrees
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 180);
                currDirection = true;
            } else{
                Debug.Log(Mathf.RoundToInt(transform.rotation.eulerAngles.z));
            }
        }
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
