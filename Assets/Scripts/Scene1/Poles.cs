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

        // Start is called before the first frame update
        private void Start()
        {
        }

        private void OnMouseOver()
        {
            intText3.GetComponent<TMP_Text>().text = "POLES NEEDED TO INTERACT";
            if (!ladderUI.activeSelf)
            {
                intText3.SetActive(IsWithinReach());
            }
            else
            {
                intText3.SetActive(false);
            }

            if (Input.GetMouseButtonDown(1) && IsWithinReach() && Player.Player.Instance.GetHeldItem().itemName == "Poles")
            {
                ladderUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                PauseMenu.IsPaused = true;
            }
        }

        private void OnMouseExit()
        {
            intText3.SetActive(false);
        }

        private void Update()
        {
            if (ladderUI.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ladderUI.SetActive(false);
                    PauseMenu.IsPaused = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }

        private bool IsWithinReach()
        {
            return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
        }
    }
}