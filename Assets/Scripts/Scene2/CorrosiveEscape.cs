using System.Collections;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CorrosiveEscape : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText3;
    [SerializeField] private Animator escapeAnim;
    [SerializeField] private GameObject cer;
    [SerializeField] private GameObject escapeText;
    [SerializeField] private GameObject escapeUI;

    private AudioSource[] _allAudioSources;
    private static readonly int IsEscape = Animator.StringToHash("isEscape");

    private void OnMouseExit()
    {
        intText3.SetActive(false);
    }

    private void OnMouseOver()
    {
        if(IsWithinReach()){

            intText3.GetComponent<TMP_Text>().text = "CORROSIVE NEEDED TO INTERACT";
            intText3.SetActive(true);

            if (Player.Player.Instance.EpicModeEnabled() && Input.GetMouseButtonDown(1))
                StartCoroutine(EscapeFunc());

            if (Input.GetMouseButtonDown(1) && IsWithinReach() && Player.Player.Instance.IsHolding("Corrosive"))
                StartCoroutine(EscapeFunc());

        }
    }

    public IEnumerator EscapeFunc()
    {
        escapeUI.SetActive(true);
        cer.SetActive(false);
        _allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        if (_allAudioSources != null)
            foreach (var audioS in _allAudioSources)
                audioS.Stop();

        escapeAnim.SetBool(IsEscape, true);
        yield return new WaitForSeconds(1.5f);
        SaveSystem.SaveLevel(3, Player.Player.Instance.GetTime());
        escapeText.GetComponent<TMP_Text>().text = "ESCAPED\nIn " + Player.Player.Instance.GetTime();
        escapeText.SetActive(true);
        yield return new WaitUntil(() => Input.anyKeyDown);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
