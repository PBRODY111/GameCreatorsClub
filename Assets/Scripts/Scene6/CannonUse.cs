using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonUse : MonoBehaviour
{
    [SerializeField] private GameObject shockLight;
    [SerializeField] private AudioSource shockAudio;
    [SerializeField] private bool canShock = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && canShock){
            StartCoroutine(ActivateShock());
        }
    }

    IEnumerator ActivateShock(){
        canShock = false;
        shockAudio.Play();
        shockLight.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        shockLight.SetActive(false);
        shockAudio.Stop();
        canShock = true;
    }
}
