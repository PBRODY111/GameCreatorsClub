using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Breaker0 : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText;
    [SerializeField] private GameObject powerText;
    [SerializeField] private Button powerButton;
    [SerializeField] private bool hasPower;
    [SerializeField] private AudioSource breakerAudio;
    [SerializeField] private AudioSource breakerAudio2;
    [SerializeField] private AudioClip [] breakerSounds;
    
    // Start is called before the first frame update
    void Start()
    {
        hasPower = true;
        StartCoroutine(SetBreaker());
    }

    private void OnMouseExit()
    {
        intText.SetActive(false);
    }

    private void OnMouseOver()
    {
        intText.SetActive(IsWithinReach() && !hasPower);
        if (Input.GetKeyDown(KeyCode.E) && IsWithinReach() && !hasPower)
        {
            StartCoroutine(SetBreaker());
        }
    }

    private IEnumerator SetBreaker()
    {
        transform.rotation = Quaternion.Euler(-90f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        breakerAudio.Play();
        breakerAudio2.clip = breakerSounds[0];
        breakerAudio2.Play();
        hasPower = true;
        powerButton.interactable = true;
        powerText.GetComponent<TMP_Text>().text = "POWER CONNECTED";
        yield return new WaitForSeconds(Random.Range(25f, 35f));
        hasPower = false;
        powerButton.interactable = false;
        powerText.GetComponent<TMP_Text>().text = "POWER DISCONNECTED";
        transform.rotation = Quaternion.Euler(-220f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        breakerAudio.Play();
        breakerAudio2.clip = breakerSounds[1];
        breakerAudio2.Play();
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
