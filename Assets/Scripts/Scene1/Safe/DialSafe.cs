using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Scene1.Safe
{
    public class DialSafe : Safe
    {
        
        [SerializeField] private float[] dialValues;
        
        private int _currentDialValueIndex;
        
        [SerializeField] private Slider _slider;
        private float _sliderPreviousValue;
        
        
        

        private new void Awake()
        {
            base.Awake();
            _slider.onValueChanged.AddListener(delegate
            {
                if (IsWithinValue(_sliderPreviousValue, _slider.value, 1f)) GetComponent<AudioSource>().Play();
                _sliderPreviousValue = _slider.value;
            });
        }

        private new void Update()
        {
            base.Update();
            if (ui.activeSelf) HandleMouseUp();
        }

        private void HandleMouseUp()
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (IsWithinValue(dialValues[_currentDialValueIndex], _slider.value, 2f))
                {
                    _currentDialValueIndex++;
                    if (_currentDialValueIndex >= dialValues.Length) OpenSafe();
                }
                else _currentDialValueIndex = 0;
                _slider.value = 0;
            }
        }
        private static bool IsWithinValue(float value, float actual, float deviation) => actual >= value - deviation && actual <= value + deviation;
    }
}