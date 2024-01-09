using UnityEngine;

namespace Scene1
{
    public class CameraShake : MonoBehaviour
    {
        // Transform of the camera to shake. Grabs the gameObject's transform
        // if null.
        public Camera cerCamera;
        public Transform camTransform;

        // How long the object should shake for.
        public float shakeDuration;

        // Amplitude of the shake. A larger value shakes the camera harder.
        public float shakeAmount = 0.7f;
        public float decreaseFactor = 1.0f;

        private Vector3 _originalPos;

        private void Awake()
        {
            if (camTransform == null) camTransform = GetComponent(typeof(Transform)) as Transform;
        }

        private void Update()
        {
            if (Camera.main == cerCamera) Debug.Log("Breuh");

            if (shakeDuration < 0)
            {
                camTransform.localPosition = _originalPos + Random.insideUnitSphere * shakeAmount;

                shakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shakeDuration = 0f;
                camTransform.localPosition = _originalPos;
            }
        }

        private void OnEnable()
        {
            _originalPos = camTransform.localPosition;
        }
    }
}