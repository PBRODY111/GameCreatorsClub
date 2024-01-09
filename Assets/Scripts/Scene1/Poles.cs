using TMPro;
using UI;
using UnityEngine;

namespace Scene1
{
    public class Poles : MonoBehaviour
    {
        [SerializeField] private float reach;
        [SerializeField] private GameObject intText3;

        [SerializeField] private GameObject ladderUI;

        private void Update()
        {
            if (ladderUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            {
                ladderUI.SetActive(false);
                PauseMenu.IsPaused = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        private void OnMouseExit()
        {
            intText3.SetActive(false);
        }

        private void OnMouseOver()
        {
            intText3.GetComponent<TMP_Text>().text = "POLES NEEDED TO INTERACT";
            intText3.SetActive(!ladderUI.activeSelf && IsWithinReach());

            if (Input.GetMouseButtonDown(1) && IsWithinReach() &&
                Player.Player.Instance.IsHolding("Poles"))
            {
                ladderUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                PauseMenu.IsPaused = true;
            }
        }

        private bool IsWithinReach()
        {
            return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
        }
    }
}