using TMPro;
using UI;
using UnityEngine;

namespace Scene1
{
    public class Poles : MonoBehaviour
    {
        [SerializeField] private float reach;
        [SerializeField] private GameObject intText3;
        [SerializeField] private GameObject intText;

        [SerializeField] private GameObject ladderUI;
        private bool hasPoles = false;

        void Update()
        {
            if (ladderUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            {
                ladderUI.SetActive(false);
                PauseMenu.IsPaused = false;
                Player.Player.Instance.LockCursor();
                Player.Player.Instance.EnableMovement();
            }
        }

        private void OnMouseExit()
        {
            intText3.SetActive(false);
        }

        private void OnMouseOver()
        {
            if(!hasPoles){
                intText3.GetComponent<TMP_Text>().text = "POLES NEEDED TO INTERACT";
                intText3.SetActive(!ladderUI.activeSelf && IsWithinReach());

                if (Input.GetMouseButtonDown(1) && IsWithinReach() && Player.Player.Instance.IsHolding("Poles"))
                {
                    ladderUI.SetActive(true);
                    hasPoles = true;
                    PauseMenu.IsPaused = true;
                    Player.Player.Instance.DisableMovement();
                    Player.Player.Instance.UnlockCursor();
                }
            } else{
                intText.SetActive(!ladderUI.activeSelf && IsWithinReach());
                if (Input.GetKeyDown(KeyCode.E) && IsWithinReach())
                {
                    ladderUI.SetActive(true);
                    hasPoles = true;
                    PauseMenu.IsPaused = true;
                    Player.Player.Instance.DisableMovement();
                    Player.Player.Instance.UnlockCursor();
                }
            }
        }

        private bool IsWithinReach()
        {
            return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
        }
    }
}