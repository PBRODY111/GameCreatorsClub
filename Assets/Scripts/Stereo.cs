using UnityEngine;

public class Stereo : MonoBehaviour
{
    [SerializeField] private GameObject intText;
    [SerializeField] private AudioSource stereoAudio;
    [SerializeField] private AudioSource clickAudio;
    [SerializeField] private float reach = 1.5f;

    private void OnMouseExit()
    {
        intText.SetActive(false);
    }

    private void OnMouseOver()
    {
        intText.SetActive(IsWithinReach());
        if (!Input.GetKeyDown(KeyCode.E) || !IsWithinReach()) return;

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

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}