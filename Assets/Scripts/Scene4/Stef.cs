using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stef : MonoBehaviour
{
    [SerializeField] private int chance;
    // Start is called before the first frame update
    void Start()
    {
        chance = Random.Range(0, 20);
        Cursor.lockState = CursorLockMode.None;
        if(chance != 1){
            gameObject.SetActive(false);
        }
    }
}
