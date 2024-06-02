using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerTarget : MonoBehaviour
{
    [SerializeField] private AudioSource warningAudio;
    public bool canAttack = false;
    
    void Start()
    {
        // Assume the player has a tag "Player"
        StartCoroutine(UpdatePositionRoutine());
    }

    IEnumerator UpdatePositionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(7.0f); // Wait for 7 seconds
            canAttack = false;
            warningAudio.Play();
            transform.position = Player.Player.Instance.transform.position;
            yield return new WaitForSeconds(0.01f);
            transform.position = gameObject.transform.position;
            yield return new WaitForSeconds(1f);
            canAttack = true;
        }
    }
}
