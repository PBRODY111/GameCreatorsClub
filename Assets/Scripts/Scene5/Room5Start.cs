using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room5Start : MonoBehaviour
{
    [SerializeField] private AudioSource myraAudio;
    [SerializeField] private AudioSource cerberusAudio;
    [SerializeField] private Animator doorAnim;
    [SerializeField] private Animator myraAnim;
    [SerializeField] private Animator armAnim;
    [SerializeField] private GameObject cerberus;
    [SerializeField] private GameObject blockade;
    [SerializeField] private GameObject myra;
    [SerializeField] private GameObject myraTarget; // Assign the target GameObject in the Inspector
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private MusicBox box;
    private static readonly int IsOpen = Animator.StringToHash("isOpen");
    private static readonly int IsDead = Animator.StringToHash("isDead");
    private static readonly int IsKill = Animator.StringToHash("isKill");
    private bool hasEntered = false;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Zagreus" && !hasEntered)
        {
            hasEntered = true;
            StartCoroutine(IntroStart());
        }
    }

    IEnumerator IntroStart(){
        myraAudio.Play();
        yield return new WaitForSeconds(2f);
        cerberusAudio.Play();
        armAnim.SetBool(IsKill, true);
        yield return new WaitForSeconds(0.2f);
        myraAnim.SetBool(IsDead, true);
        yield return new WaitForSeconds(0.8f);
        doorAnim.SetBool(IsOpen, true);
        StartCoroutine(MoveToTargetPosition());
        blockade.SetActive(false);
        yield return new WaitForSeconds(1f);
        box.isActive = true;
        cerberus.SetActive(true);
    }

    private IEnumerator MoveToTargetPosition()
    {
        Vector3 startPosition = myra.transform.position;
        Vector3 targetPosition = myraTarget.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            myra.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position is set to the target position
        myra.transform.position = targetPosition;
    }
}
