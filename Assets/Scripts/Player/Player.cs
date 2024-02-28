using System;
using JetBrains.Annotations;
using Player.Inventory;
using UI;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Serialization;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public static Player Instance;

        [FormerlySerializedAs("_hotbar")]
        [SerializeField]
        public GameObject hotbar;

        public Camera mainCamera;
        public GameObject stoolPrefab;
        public GameObject ladderPrefab;
        public int selectedslot;

        private PlayerMovement _playerMovement;
        private PlayerCam _playerCam;

        private float _timer;
        
        public void ResetTimer()
        {
            _timer = Time.time;
        }
        
        public string GetTime()
        {
            var time = Time.time - _timer;
            var minutes = Mathf.FloorToInt(time / 60);
            var seconds = Mathf.FloorToInt(time % 60);
            var milliseconds = Mathf.FloorToInt(time * 100 % 100);
            return $"{minutes:00}:{seconds:00}.{milliseconds:00}";
        }

        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
            Instance = this;
            _timer = Time.time;
            selectedslot = -1;
            _playerMovement = GetComponent<PlayerMovement>();
            _playerCam = mainCamera.GetComponent<PlayerCam>();
            mainCamera.fieldOfView = 70;
            _originalFOV = 70;
        }
        
        public void DisableMovement()
        {
            _playerMovement.enabled = false;
            _playerCam.enabled = false;
        }
        
        public void EnableMovement()
        {
            _playerMovement.enabled = true;
            _playerCam.enabled = true;
        }
        
        public void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        public void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void Update()
        {
            try
            {
                if (Input.GetKeyDown(KeyCode.Mouse1) && selectedslot != -1) hotbar.transform.GetChild(selectedslot).GetComponent<InventoryItemController>().UseItem();
                if (Input.GetKeyDown(KeyCode.Q)) hotbar.transform.GetChild(selectedslot).GetComponent<InventoryItemController>().DropItem();

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
                Debug.Log("No Item in Slot " + e);
            }
        }
        
        private float _originalFOV;
        
        public void ChangeFOV(float fov)
        {
            mainCamera.fieldOfView = fov;
            _originalFOV = fov;
        }

        public void ToggleRespectablePostProcessing()
        {
            var volume = mainCamera.GetComponent<PostProcessVolume>();
            volume.profile.settings.ForEach(setting =>
            {
                setting.enabled.value = !setting.enabled.value;
            });
        }
        
        // OPTIFINE ZOOM HERE
        private void FixedUpdate()
        {
            if(!DebugUI.DebugActive()) return;
            
            if(Input.GetKeyDown(KeyCode.C))
            {
                _originalFOV = mainCamera.fieldOfView;
                mainCamera.fieldOfView = 20;
            }
            
            if (Input.GetKey(KeyCode.C))
            {
                var fieldOfView = mainCamera.fieldOfView;
                fieldOfView -= Input.mouseScrollDelta.y * 30;
                mainCamera.fieldOfView = Mathf.Clamp(fieldOfView, 1, _originalFOV);
            }
            else
            {
                mainCamera.fieldOfView = _originalFOV;
            }
        }

        [CanBeNull]
        public InventoryItem GetHeldItem()
        {
            try
            {
                return hotbar.transform.GetChild(selectedslot).GetComponent<InventoryItemController>().item;
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public bool IsHolding(InventoryItem item)
        {
            return GetHeldItem() == item || DebugUI.DebugActive();
        }
        
        public bool IsHolding(string itemName)
        {
            var heldItem = GetHeldItem();
            var isHeld = (heldItem is not null && heldItem.itemName == itemName) || DebugUI.DebugActive();
            Debug.Log($"Is holding {itemName}: {isHeld}");
            return isHeld;
        }

        public bool EpicModeEnabled()
        {
            return _playerMovement.epicModeEnabled;
        }
    }
}