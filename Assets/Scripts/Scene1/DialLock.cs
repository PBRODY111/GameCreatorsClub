using UnityEngine;
using UnityEngine.UI;

namespace Scene1
{
    public class DialLock : MonoBehaviour
    {
        public GameObject dialLock;
        public GameObject dialUI;
        public float interactionDistance;
        public GameObject intText;
        public Slider dialSlider;
        public string doorOpenAnimName;
        public float dialSliderGet;
        public float firstValue;
        public float secondValue;
        public float thirdValue;

        public float tempValue;

        private void Start()
        {
            intText.SetActive(false);
            dialSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        }

        private void Update()
        {
            HandleRaycast();
            HandleMouseInput();
        }

        private void HandleRaycast()
        {
            var transform1 = transform;
            var ray = new Ray(transform1.position, transform1.forward);
            if (Physics.Raycast(ray, out var hit, interactionDistance))
            {
                if (hit.collider.gameObject.name == dialLock.name)
                {
                    intText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        dialUI.SetActive(true);
                        Cursor.lockState = CursorLockMode.None;
                    }
                }
            }
        }

        private void HandleMouseInput()
        {
            if (Input.GetMouseButton(0)) return;
            
            if (tempValue != 0)
            {
                AssignTempValue();
                return;
            }

            if (firstValue == 0 || secondValue == 0 || thirdValue == 0) return;
            
            dialUI.SetActive(false);
            if (firstValue is >= 16 and <= 20 && secondValue is >= 23 and <= 27 && thirdValue is >= 3 and <= 7)
            {
                var safeAnimator = dialLock.GetComponent<Animator>();
                safeAnimator.SetBool(doorOpenAnimName, true);
            }

            firstValue = secondValue = thirdValue = 0;
        }

        private void AssignTempValue()
        {
            if (firstValue == 0)
                firstValue = tempValue;
            else if (secondValue == 0)
                secondValue = tempValue;
            else
                thirdValue = tempValue;

            dialSlider.value = 0;
        }

        private void ValueChangeCheck()
        {
            tempValue = dialSlider.value;
        }
    }
}