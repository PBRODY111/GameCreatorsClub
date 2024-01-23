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
        [SerializeField] private GameObject ladderObj;

        private bool hasPoles = false;

        private Transform screwTransform;
        private Transform screw1Transform;
        private Transform screw2Transform;
        private Transform screw3Transform;
        private Transform screw4Transform;

        void Start()
        {
            // Initialize the Transform variables here
            screwTransform = ladderUI.transform.Find("Screw");
            screw1Transform = ladderUI.transform.Find("Screw (1)");
            screw2Transform = ladderUI.transform.Find("Screw (2)");
            screw3Transform = ladderUI.transform.Find("Screw (3)");
            screw4Transform = ladderUI.transform.Find("Screw (4)");
        }

        void Update()
        {
            if (ladderUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            {
                ladderUI.SetActive(false);
                PauseMenu.IsPaused = false;
                Player.Player.Instance.LockCursor();
                Player.Player.Instance.EnableMovement();
            }
            if(CheckScrewConditions(screwTransform)&&CheckScrew1Conditions(screw1Transform)&&CheckScrew2Conditions(screw2Transform)&&CheckScrew3Conditions(screw3Transform)&&CheckScrew4Conditions(screw4Transform))
            {
                ladderUI.SetActive(false);
                ladderObj.SetActive(true);
                PauseMenu.IsPaused = false;
                Player.Player.Instance.LockCursor();
                Player.Player.Instance.EnableMovement();
                intText.SetActive(false);
                intText3.SetActive(false);
                gameObject.SetActive(false);
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

        bool CheckScrewConditions(Transform screwTransform)
        {
            // Check if Z rotation is 45 or -135 and width is 50
            return (Mathf.Approximately(screwTransform.localRotation.eulerAngles.z, 45f) ||
                    Mathf.Approximately(screwTransform.localRotation.eulerAngles.z, -135f)) &&
                Mathf.Approximately(screwTransform.GetComponent<RectTransform>().rect.width, 50f);
        }

        bool CheckScrew3Conditions(Transform screw3Transform)
        {
            // Check if Z rotation is 90 or -90 and width is 50
            return (Mathf.Approximately(screw3Transform.localRotation.eulerAngles.z, 90f) ||
                    Mathf.Approximately(screw3Transform.localRotation.eulerAngles.z, -90f)) &&
                Mathf.Approximately(screw3Transform.GetComponent<RectTransform>().rect.width, 50f);
        }

        bool CheckScrew1Conditions(Transform screw1Transform)
        {
            // Check if Z rotation is 90 or -90 and width is 50
            return (Mathf.Approximately(screw1Transform.localRotation.eulerAngles.z, -45f) ||
                    Mathf.Approximately(screw1Transform.localRotation.eulerAngles.z, 135f)) &&
                Mathf.Approximately(screw1Transform.GetComponent<RectTransform>().rect.width, 50f);
        }

        bool CheckScrew4Conditions(Transform screw4Transform)
        {
            // Check if Z rotation is 90 or -90 and width is 50
            return (Mathf.Approximately(screw4Transform.localRotation.eulerAngles.z, 0f) ||
                    Mathf.Approximately(screw4Transform.localRotation.eulerAngles.z, 180f)) &&
                Mathf.Approximately(screw4Transform.GetComponent<RectTransform>().rect.width, 80f);
        }

        bool CheckScrew2Conditions(Transform screw2Transform)
        {
            // Check if Z rotation is 90 or -90 and width is 50
            return (Mathf.Approximately(screw2Transform.localRotation.eulerAngles.z, 90f) ||
                    Mathf.Approximately(screw2Transform.localRotation.eulerAngles.z, -90f)) &&
                Mathf.Approximately(screw2Transform.GetComponent<RectTransform>().rect.width, 80f);
        }
    }
}