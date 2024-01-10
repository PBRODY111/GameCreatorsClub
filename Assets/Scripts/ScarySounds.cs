using UnityEngine;

public class ScarySounds : MonoBehaviour
{
    [SerializeField] private AudioSource doorAudio;
    public AudioClip[] sounds;
    public float period;
    private float _nextActionTime;

    // Update is called once per frame
    private void Update()
    {
        if (Time.time > _nextActionTime){
            _nextActionTime += period + Random.Range(0, 10);
            doorAudio.clip = sounds[Random.Range(0, sounds.Length)];
            doorAudio.Play();
        }
    }
}