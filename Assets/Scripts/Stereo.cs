using UnityEngine;

public class Stereo : MonoBehaviour
{
    [SerializeField] private GameObject intText;
    [SerializeField] private AudioSource stereoAudio;
    [SerializeField] private AudioSource clickAudio;
    [SerializeField] private float reach = 1.5f;
    void OnMouseOver()
    {
        intText.SetActive(IsWithinReach());
        if (Input.GetKeyDown(KeyCode.E) && IsWithinReach())
        {
            if (stereoAudio.isPlaying)
            {
                stereoAudio.Pause();
                clickAudio.Play(0);
            }
            else
            {
                stereoAudio.Play(0);
                clickAudio.Play(0);
            }
        }
    }
    void OnMouseExit()
    {
        intText.SetActive(false);
    }

    bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position) <= reach;
    }
}
