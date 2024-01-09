using UnityEngine;

public class DeskSound : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioSource impactAudio;

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 3f)
        {
            impactAudio.Play();
        }
    }
}