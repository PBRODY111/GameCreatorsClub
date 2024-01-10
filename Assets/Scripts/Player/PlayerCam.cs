using UI;
using UnityEngine;

namespace Player
{
    public class PlayerCam : MonoBehaviour
    {
        public float sensX;
        public float sensY;

        public Transform orientation;
        public float initialY;

        private float _xRotation;
        private float _yRotation;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _xRotation = 0;
            _yRotation = initialY;
        }

        private void Update()
        {
            if (PauseMenu.IsPaused) return;
            
            var mouseX = Input.GetAxis("Mouse X") * 0.02f * sensX;
            var mouseY = Input.GetAxis("Mouse Y") * 0.02f * sensY;

            _yRotation += mouseX;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, _yRotation, 0);
        }
    }
}