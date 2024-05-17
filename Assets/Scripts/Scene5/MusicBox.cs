using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MusicBox : MonoBehaviour
{
    public int count = 60;
    public int maxCount = 60;
    private Coroutine increaseCoroutine;
    private Coroutine decreaseCoroutine;
    public bool isActive = false;
    [SerializeField] private AudioSource waltz;
    [SerializeField] private AudioSource windup;
    [SerializeField] private Animator handle;
    [SerializeField] private TMP_Text timer;
    [SerializeField] private GameObject intText3;
    [SerializeField] private float reach;
    [SerializeField] private WeepingAngel cerberus;
    private bool isInBox = false;

    void Update()
    {
        if(count == 0){
            waltz.Pause();
            StartCoroutine(cerberus.MusicBoxJumpscare());
        } else{
            if(isActive && !waltz.isPlaying)
                waltz.Play();
        }
        if(!isInBox){
            windup.Pause();
            handle.speed = 0;
            if (increaseCoroutine != null)
            {
                // Stop the increase coroutine if it's running
                StopCoroutine(increaseCoroutine);
                increaseCoroutine = null;
            }

            if (decreaseCoroutine == null)
            {
                // Start the coroutine to decrease the count if it's not already running
                decreaseCoroutine = StartCoroutine(DecreaseCount());
            }
        }
    }

    void OnMouseOver(){
        if(IsWithinReach()){
            intText3.GetComponent<TMP_Text>().text = "HOLD [E] TO WIND UP";
            intText3.SetActive(true);
        }
        if (Input.GetKey(KeyCode.E) && IsWithinReach())
        {
            isInBox = true;
            if (increaseCoroutine == null)
            {
                // Start the coroutine to increase the count if it's not already running
                increaseCoroutine = StartCoroutine(IncreaseCount());
            }
        } else{
            isInBox = false;
        }
    }
    void OnMouseExit(){
        intText3.SetActive(false);
        isInBox = false;
    }

    private IEnumerator IncreaseCount()
    {
        while (true)
        {
            if (count < maxCount)
            {
                if(!windup.isPlaying)
                    windup.Play();
                handle.speed = 1;
                count++;
                timer.text = ""+count;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    private IEnumerator DecreaseCount()
    {
        while (true && isActive)
        {
            if (count > 0)
            {
                count--;
                timer.text = ""+count;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
