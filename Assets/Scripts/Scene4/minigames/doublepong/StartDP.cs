using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDP : MonoBehaviour
{
    [SerializeField] private GameObject gameElements;

    // Update is called once per frame
    private void Update(){
        if (Input.GetKeyDown(KeyCode.Space)){
            gameElements.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
