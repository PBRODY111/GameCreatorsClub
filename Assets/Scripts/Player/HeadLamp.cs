using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class HeadLamp : MonoBehaviour
    {
        public float batteryDrain = 100f;
        [FormerlySerializedAs("_lightStage")] public int lightStage = 4;

        [FormerlySerializedAs("_lightParent")]
        [SerializeField]
        private GameObject lightParent;

        [FormerlySerializedAs("_canvas")]
        [SerializeField]
        private Canvas canvas;

        [FormerlySerializedAs("_batteryBarPrefab")]
        [SerializeField]
        private GameObject batteryBarPrefab;

        private float _batteryLife = 3999f;

        private Color _color;

        private bool _fullbright;
        private bool _flashlightOn = true;
        private Light[] _lights;
        private GameObject[] _lightObj;

        private void Start()
        {
            _lights = new Light[lightParent.transform.childCount];
            for (var i = 0; i < _lights.Length; i++)
                _lights[i] = lightParent.transform.GetChild(i).GetComponent<Light>();

            _lightObj = new GameObject[3];
            for (var i = 0; i < _lightObj.Length; i++)
                _lightObj[i] = lightParent.transform.GetChild(i).gameObject;

            _color = _lights[0].color;
        }

        private void Update()
        {
            if (!_flashlightOn) return;
            if (_fullbright)
            {
                _batteryLife = 4000f;
                return;
            }

            if (_batteryLife > -1000f)
                _batteryLife -= Time.deltaTime * batteryDrain;
            lightStage = Mathf.CeilToInt(_batteryLife / 1000f);
            _lights[0].intensity = 0.2f * (lightStage + 1);
            _lights[1].intensity = 0.07f * (lightStage + 1);
            _lights[2].intensity = 0.07f * (lightStage + 1);
            if (canvas.transform.GetChild(0).childCount == lightStage + 1) return;

            var temp = canvas.transform.GetChild(0).childCount;
            for (var i = 0; i < temp; i++) Destroy(canvas.transform.GetChild(0).GetChild(i).gameObject);

            for (var i = 0; i < lightStage + 1; i++) Instantiate(batteryBarPrefab, canvas.transform.GetChild(0));
        }

        private void FixedUpdate(){
            if (Input.GetKeyDown(KeyCode.F)){
                _lightObj[0].SetActive(!_flashlightOn);
                _lightObj[1].SetActive(!_flashlightOn);
                _lightObj[2].SetActive(!_flashlightOn);
                _flashlightOn = !_flashlightOn;
            }
        }

        public void Fullbright()
        {
            if (_fullbright)
            {
                _fullbright = false;
                foreach (var t in _lights)
                {
                    t.spotAngle = 60f;
                    t.range = 10f;
                    t.color = _color;
                }

                return;
            }

            _fullbright = true;
            foreach (var t in _lights)
            {
                t.intensity = 0.6f;
                t.spotAngle = 179.999f;
                t.range = 100f;
                t.color = Color.white;
            }
        }

        public void Charge(float charge)
        {
            _batteryLife += charge;
            if (_batteryLife > 4000f) _batteryLife = 4000f;
        }
    }
}