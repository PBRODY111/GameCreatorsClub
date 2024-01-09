using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Vent : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText;
    [SerializeField] private GameObject intText3;
    [SerializeField] private Animator cerAnimator;
    [SerializeField] private Animator escapeAnim;
    [SerializeField] private Attack1 attack1;
    [SerializeField] private GameObject cer;
    [SerializeField] private GameObject gas;
    [SerializeField] private GameObject escapeText;
    [SerializeField] private GameObject escapeUI;
    private AudioSource[] _allAudioSources;
    public AudioSource growlAudio;
    public AudioSource doorSlam;
    public AudioSource footsteps;

    public int unscrewed;

    // Start is called before the first frame update
    private void Start()
    {
    }

    private void OnMouseOver()
    {
        if (unscrewed >= 4)
        {
            intText3.GetComponent<TMP_Text>().text = "CROWBAR NEEDED TO INTERACT";
            intText3.SetActive(true);

            if (Player.Instance.EpicModeEnabled() && Input.GetMouseButtonDown(1))
            {
                StartCoroutine(EscapeFunc());
            }

            if (Input.GetMouseButtonDown(1) && IsWithinReach() && Player.Instance.GetHeldItem().itemName == "Crowbar")
            {
                StartCoroutine(EscapeFunc());
            }
        }
    }

    private void OnMouseExit()
    {
        intText.SetActive(false);
        intText3.SetActive(false);
    }

    private void Update()
    {
        if (unscrewed >= 4)
        {
            PauseMenu.IsPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            cerAnimator.SetBool("isLeaving", true);
            attack1.jumpscare = true;
            attack1.t = 0;
        }
    }

    private IEnumerator EscapeFunc()
    {
        escapeUI.SetActive(true);
        cer.SetActive(false);
        gas.SetActive(false);
        _allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (var audioS in _allAudioSources)
        {
            audioS.Stop();
        }

        escapeAnim.SetBool("isEscape", true);
        yield return new WaitForSeconds((float)1.5);
        escapeText.SetActive(true);
        yield return new WaitForSeconds((float)1.5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position) <= reach;
    }
}