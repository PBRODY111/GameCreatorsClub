using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cassette : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText;
    [SerializeField] private AudioSource cassetteAudio;
    [SerializeField] private AudioSource clickAudio;
    // Start is called before the first frame update
    private void OnMouseOver()
    {
        intText.SetActive(IsWithinReach());
        if (Input.GetKeyDown(KeyCode.E) && IsWithinReach())
        {
            StartCoroutine(AudioSequence());
        }
    }

    private IEnumerator AudioSequence(){
        clickAudio.Play();
        yield return new WaitForSeconds(3);
        cassetteAudio.Play();
    }
    private void OnMouseExit()
    {
        intText.SetActive(false);
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
