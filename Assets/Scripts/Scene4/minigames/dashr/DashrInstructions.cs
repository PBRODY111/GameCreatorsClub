using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashrInstructions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CloseAfter());
    }

    // Update is called once per frame
    IEnumerator CloseAfter(){
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }
}
