using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ascal : MonoBehaviour
{
    [SerializeField] private GameObject ascalUI;
    [SerializeField] private AudioSource ascalAudio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EnterAscal(){
        ascalUI.SetActive(true);
        ascalAudio.Play();
    }

    public void ExitAscal(){
        ascalUI.SetActive(false);
        ascalAudio.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
