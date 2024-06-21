using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDashr : MonoBehaviour
{
    [SerializeField] private GameObject gameElements;
    [SerializeField] private GameObject hunterObj;
    [SerializeField] private Hunter hunter;

    // Update is called once per frame
    private void Update(){
        if (Input.GetKeyDown(KeyCode.Space)){
            gameElements.SetActive(true);
            StartCoroutine(hunter.SwitchImage());
            hunterObj.transform.localPosition = new Vector3(-463f, -288f, hunterObj.transform.position.z);
            gameObject.SetActive(false);
        }
    }
}
