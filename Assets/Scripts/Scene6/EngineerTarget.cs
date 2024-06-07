using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerTarget : MonoBehaviour
{
    [SerializeField] private AudioSource warningAudio;
    [SerializeField] private GameObject point1;
    [SerializeField] private GameObject point2;
    [SerializeField] private GameObject point3;
    public bool canAttack = false;
    public bool finalStage = false;
    
    void Start()
    {
        // Assume the player has a tag "Player"
        StartCoroutine(UpdatePositionRoutine());
    }

    IEnumerator UpdatePositionRoutine()
    {
        while (true)
        {
            if(!finalStage){
                yield return new WaitForSeconds(6.0f); // Wait for 6 seconds
                canAttack = false;
                warningAudio.Play();
                transform.position = Player.Player.Instance.transform.position;
                yield return new WaitForSeconds(0.01f);
                transform.position = gameObject.transform.position;
                yield return new WaitForSeconds(1f);
                canAttack = true;
            } else{
                yield return new WaitForSeconds(4.0f);
                canAttack = false;
                warningAudio.Play();
                transform.position = point1.transform.position;
                yield return new WaitForSeconds(0.8f);
                canAttack = true;
                yield return new WaitForSeconds(0.3f);
                transform.position = point2.transform.position;
                yield return new WaitForSeconds(0.3f);
                transform.position = point3.transform.position;
                yield return new WaitForSeconds(0.3f);
                transform.position = Player.Player.Instance.transform.position;
                yield return new WaitForSeconds(0.01f);
                transform.position = gameObject.transform.position;
            }
        }
    }
}
