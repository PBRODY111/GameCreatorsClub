using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarySounds : MonoBehaviour
{
    [SerializeField] private AudioSource doorAudio;
    public AudioClip[] sounds;
    private float nextActionTime;
    public float period;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime ) {
            nextActionTime += (period + Random.Range(0,10));
            doorAudio.clip = sounds[Random.Range(0,sounds.Length)];
            doorAudio.Play();
        }
    }
}
