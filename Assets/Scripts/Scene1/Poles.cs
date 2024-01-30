using TMPro;
using UI;
using UnityEngine;
using Player.Inventory;

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

        private Transform screw0Transform;
        private Transform screw1Transform;
        private Transform screw2Transform;
        private Transform screw3Transform;
        private Transform screw4Transform;
        private float zRotation0;
        private float zRotation1;
        private float zRotation2;
        private float zRotation3;
        private float zRotation4;
        private float epsilon = 0.001f;

        void Start()
        {
            // Initialize the Transform variables here
            screw0Transform = ladderUI.transform.Find("Screw (0)");
            screw1Transform = ladderUI.transform.Find("Screw (1)");
            screw2Transform = ladderUI.transform.Find("Screw (2)");
            screw3Transform = ladderUI.transform.Find("Screw (3)");
            screw4Transform = ladderUI.transform.Find("Screw (4)");
        }

        void Update()
        {
            zRotation0 = screw0Transform.rotation.eulerAngles.z;
            zRotation1 = screw1Transform.rotation.eulerAngles.z;
            zRotation2 = screw2Transform.rotation.eulerAngles.z;
            zRotation3 = screw3Transform.rotation.eulerAngles.z;
            zRotation4 = screw4Transform.rotation.eulerAngles.z;
            if (ladderUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            {
                ladderUI.SetActive(false);
                PauseMenu.IsPaused = false;
                Player.Player.Instance.LockCursor();
                Player.Player.Instance.EnableMovement();
            }
            if(CheckScrew0Conditions(screw0Transform)&&CheckScrew1Conditions(screw1Transform)&&CheckScrew2Conditions(screw2Transform)&&CheckScrew3Conditions(screw3Transform)&&CheckScrew4Conditions(screw4Transform))
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
                    Player.Player.Instance.hotbar.transform.GetChild(selectedslot).GetComponent<InventoryItemController>().RemoveItem();
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
            Debug.Log((Approximately(zRotation3, 90f, epsilon) || Approximately(zRotation3, -90f, epsilon)));
            Debug.Log(screw3Transform.GetComponent<RectTransform>().rect.width == 50);
        }

        private bool IsWithinReach()
        {
            return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
        }

        bool Approximately(float a, float b, float epsilon)
        {
            return Mathf.Abs(a - b) < epsilon;
        }

        bool CheckScrew0Conditions(Transform screw0Transform)
        {
            // Check if Z rotation is 45 or -135 and width is 50
            return ((Approximately(zRotation0, 45f, epsilon) || Approximately(zRotation0, -135f, epsilon)) &&
                screw0Transform.GetComponent<RectTransform>().rect.width == 50);
        }

        bool CheckScrew3Conditions(Transform screw3Transform)
        {
            // Check if Z rotation is 90 or -90 and width is 50
            return ((Approximately(zRotation3, 90f, epsilon) || Approximately(zRotation3, -90f, epsilon)) &&
                screw3Transform.GetComponent<RectTransform>().rect.width == 50);
        }

        bool CheckScrew1Conditions(Transform screw1Transform)
        {
            // Check if Z rotation is 90 or -90 and width is 50
            return ((Approximately(zRotation1, -45f, epsilon) || Approximately(zRotation1, 135f, epsilon)) &&
                screw1Transform.GetComponent<RectTransform>().rect.width == 50);
        }

        bool CheckScrew4Conditions(Transform screw4Transform)
        {
            // Check if Z rotation is 90 or -90 and width is 50
            return ((Approximately(zRotation4, 0f, epsilon) || Approximately(zRotation4, 180f, epsilon)) &&
                screw4Transform.GetComponent<RectTransform>().rect.width == 80);
        }

        bool CheckScrew2Conditions(Transform screw2Transform)
        {
            // Check if Z rotation is 90 or -90 and width is 50
            return ((Approximately(zRotation2, 90f, epsilon) || Approximately(zRotation2, -90f, epsilon)) &&
                screw2Transform.GetComponent<RectTransform>().rect.width == 80);
        }
    }
}