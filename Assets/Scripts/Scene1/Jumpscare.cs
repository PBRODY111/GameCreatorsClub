using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene1
{
    public class Jumpscare : MonoBehaviour
    {
        private static readonly int IsScared = Animator.StringToHash("isScared");
        [SerializeField] private GameObject cer;
        [SerializeField] private Animator cerAnimator;
        [SerializeField] private AudioSource jumpscareAudio;
        [SerializeField] private GameObject intText3;
        [SerializeField] private GameObject playerModel;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Camera cerCamera;

        [SerializeField] private float reach;

        private void Start()
        {
            mainCamera.enabled = true;
            cerCamera.enabled = false;
        }

        private void OnMouseExit()
        {
            intText3.SetActive(false);
        }

        private void OnMouseOver()
        {
            intText3.GetComponent<TMP_Text>().text = "CROWBAR NEEDED TO INTERACT";
            intText3.SetActive(IsWithinReach());
            if (Input.GetMouseButtonDown(1) && IsWithinReach() && Player.Player.Instance.IsHolding("Crowbar"))
            {
                Debug.Log("JUMPSCARE!!");
                cer.transform.position = new Vector3(-8.5f, 0.5f, -4f);
                //cer.SetActive(true);
                playerModel.transform.position = new Vector3(-6.5f, 0f, -4f);
                StartCoroutine(JumpscareSequence());
            }
        }

        public IEnumerator JumpscareSequence()
        {
            mainCamera.enabled = false;
            cerCamera.enabled = true;
            cerAnimator.SetBool(IsScared, true);
            jumpscareAudio.Play();
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("GameOverScene");
        }

        private bool IsWithinReach()
        {
            return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
        }
    }
}