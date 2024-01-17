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
                playerModel.transform.position = new Vector3(-6f, 0.12f, -4f);
                StartCoroutine(JumpscareSequence());
            }
        }
        
        private bool isJumpscareActive = false;

        public IEnumerator JumpscareSequence()
        {
            // Cancel any ongoing jumpscare sequence
            if (isJumpscareActive) yield break;

            isJumpscareActive = true;

            var playerPos = playerModel.transform.position;
            cer.transform.position = new Vector3(playerPos.x - 2f, playerPos.y + 0.5f, playerPos.z);
            mainCamera.enabled = false;
            cerCamera.enabled = true;
            cerAnimator.SetBool(IsScared, true);
            jumpscareAudio.Play();
            yield return new WaitForSeconds(2.25f);

            if (!isJumpscareActive) yield break;

            SceneManager.LoadScene("GameOverScene");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            isJumpscareActive = false;
        }

        private bool IsWithinReach()
        {
            return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
        }
    }
}