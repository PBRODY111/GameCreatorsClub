using System;
using Player.Inventory;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public static Player Instance;

        [FormerlySerializedAs("_hotbar")]
        [SerializeField]
        private GameObject hotbar;

        public Camera mainCamera;
        public GameObject stoolPrefab;
        public int selectedslot;

        private PlayerMovement _playerMovement;

        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
            Instance = this;
            selectedslot = -1;
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            try
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    Debug.Log(
                        hotbar.transform.GetChild(selectedslot).GetComponent<InventoryItemController>().item.itemName);
                    hotbar.transform.GetChild(selectedslot).GetComponent<InventoryItemController>().UseItem();
                }

                for (var i = 0; i < 10; i++)
                {
                    var key = (KeyCode)Enum.Parse(typeof(KeyCode), "Alpha" + (i == 9 ? 0 : i + 1));
                    if (Input.GetKeyDown(key))
                    {
                        hotbar.transform.GetChild(i).GetComponent<InventoryItemController>().HoldItem();
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("No Item in Slot" + e);
            }
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.C))
            {
                if(Math.Abs(mainCamera.fieldOfView - 70) < 0.1)
                    mainCamera.fieldOfView = 20;
                
                var scrollInput = Input.mouseScrollDelta.y;
                mainCamera.fieldOfView -= scrollInput * 30;
                mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, 1, 69);
            }
            else
            {
                mainCamera.fieldOfView = 70;
            }
        }

        public InventoryItem GetHeldItem()
        {
            return hotbar.transform.GetChild(selectedslot).GetComponent<InventoryItemController>().item;
        }

        public bool EpicModeEnabled()
        {
            return _playerMovement.epicModeEnabled;
        }
    }
}