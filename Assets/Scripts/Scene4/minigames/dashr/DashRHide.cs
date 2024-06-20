using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashRHide : MonoBehaviour
{
    [SerializeField] private float timeToRest;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HideAfterRest());
    }

    IEnumerator HideAfterRest(){
        yield return new WaitForSeconds(timeToRest);
        Destroy(gameObject);
    }
}
