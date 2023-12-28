using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerKill : MonoBehaviour
{
    public Jumpscare jumpscare;

    void OnTriggerEnter(Collider collision){
        if (collision.gameObject.name == "Zagreus")
        {
            Debug.Log("Kill!");
            StartCoroutine(jumpscare.jumpscareSequence());
        }
    }
}
