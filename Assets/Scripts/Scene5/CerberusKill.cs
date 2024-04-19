using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CerberusKill : MonoBehaviour
{
    [SerializeField] private Camera playerCam;
    [SerializeField] private Camera cerberusCamera;
    [SerializeField] private Animator cerberusAnimator;
    [SerializeField] private AudioSource jumpscareAudio;
    [SerializeField] private AudioSource cerberusAudio;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject generalUI;
    private static readonly int IsScared = Animator.StringToHash("isScared");
    private AudioSource[] _allAudioSources;
    // Start is called before the first frame update
    void Start()
    {
        playerCam.enabled = true;
        cerberusCamera.enabled = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Zagreus")
        {
            StartCoroutine(JumpscareSequence());
        }
    }
    public IEnumerator JumpscareSequence()
    {
        Debug.Log("KILL!!");
        playerCam.enabled = false;
        cerberusCamera.enabled = true;
        cerberusAnimator.SetBool(IsScared, true);
        jumpscareAudio.Play();
        yield return new WaitForSeconds(2.25f);
        _allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        if (_allAudioSources != null)
            foreach (var audioS in _allAudioSources)
                audioS.Stop();
        generalUI.SetActive(false);
        gameOver.SetActive(true);
        cerberusAudio.Play();
        yield return new WaitForSeconds(4.3f);
        SceneManager.LoadScene("GameOverScene");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
