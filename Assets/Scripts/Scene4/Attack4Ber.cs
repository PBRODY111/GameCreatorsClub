using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Attack4Ber : MonoBehaviour
{
    [SerializeField] private bool isInTrigger = false;
    [SerializeField] private GameObject ber;
    [SerializeField] private GameObject origin;
    [SerializeField] private Camera playerCam;
    [SerializeField] private Camera berCamera;
    [SerializeField] private AudioSource jumpscareAudio;
    [SerializeField] private Animator berAnimator;
    [SerializeField] private DoorCloseTrigger1 doorTrigger;
    private static readonly int IsScared = Animator.StringToHash("isScared");
    private bool notJumpscared = true;
    private bool berMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        playerCam.enabled = true;
        berCamera.enabled = false;
        ber.transform.position = origin.transform.position;
        ber.transform.rotation = Quaternion.Euler(90, 90, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInTrigger && doorTrigger.hasEntered && !berMoving)
        {
            berMoving = true;
            StartCoroutine(MoveDownCoroutine());
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Zagreus")
        {
            ber.transform.position = gameObject.transform.position;
            isInTrigger = true;
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Zagreus")
        {
            berMoving = false;
            isInTrigger = false;
            ber.transform.position = origin.transform.position;
        }
    }
    private IEnumerator MoveDownCoroutine()
    {
        while (ber.transform.position.y > 4f && isInTrigger && Time.timeScale != 0)
        {
            float newY = Mathf.MoveTowards(ber.transform.position.y, 3f, Time.deltaTime * 0.2f);
            ber.transform.position = new Vector3(ber.transform.position.x, newY, ber.transform.position.z);
            Debug.Log(ber.transform.position.y);
            yield return null;
        }
        if(ber.transform.position.y <= 4.05 && notJumpscared){
            notJumpscared = false;
            StartCoroutine(JumpscareSequence());
        }
    }
    public IEnumerator JumpscareSequence()
    {
        SaveSystem.SaveHint("ber","room4");
        Debug.Log("KILL!!");
        playerCam.enabled = false;
        berCamera.enabled = true;
        berAnimator.SetBool(IsScared, true);
        jumpscareAudio.Play();
        yield return new WaitForSeconds(2.25f);
        SceneManager.LoadScene("GameOverScene");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
