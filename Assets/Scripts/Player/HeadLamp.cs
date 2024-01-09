using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class HeadLamp : MonoBehaviour
    {
        private float _batteryLife = 3999f;
        public float batteryDrain = 100f;
        [FormerlySerializedAs("_lightStage")] public int lightStage = 4;
        [FormerlySerializedAs("_lightParent")] [SerializeField] private GameObject lightParent;
        [FormerlySerializedAs("_canvas")] [SerializeField] private Canvas canvas;
        [FormerlySerializedAs("_batteryBarPrefab")] [SerializeField] private GameObject batteryBarPrefab;
        private Light[] _lights;

        private bool _fullbright;

        private Color _color;

        private void Start()
        {
            _lights = new Light[lightParent.transform.childCount];
            for (var i = 0; i < _lights.Length; i++)
            {
                _lights[i] = lightParent.transform.GetChild(i).GetComponent<Light>();
            }

            _color = _lights[0].color;
        }

        private void Update()
        {
            if (_fullbright)
            {
                _batteryLife = 4000f;
            }
            else
            {
                if (_batteryLife > -1000f)
                    _batteryLife -= Time.deltaTime * batteryDrain;
                lightStage = Mathf.CeilToInt(_batteryLife / 1000f);
                _lights[0].intensity = 0.2f * (lightStage + 1);
                _lights[1].intensity = 0.07f * (lightStage + 1);
                _lights[2].intensity = 0.07f * (lightStage + 1);
                if (canvas.transform.GetChild(0).childCount == lightStage + 1) return;
            
                var temp = canvas.transform.GetChild(0).childCount;
                for (var i = 0; i < temp; i++)
                {
                    Destroy(canvas.transform.GetChild(0).GetChild(i).gameObject);
                }

                for (var i = 0; i < lightStage + 1; i++)
                {
                    Instantiate(batteryBarPrefab, canvas.transform.GetChild(0));
                }
            }
        }

        public void Fullbright()
        {
            if (_fullbright)
            {
                _fullbright = false;
                foreach (var t in _lights)
                {
                    t.intensity = 0.2f * (lightStage + 1);
                    t.spotAngle = 60f;
                    t.range = 20f;
                    t.color = _color;
                }
            }
            else
            {
                _fullbright = true;
                foreach (var t in _lights)
                {
                    t.intensity = 0.6f;
                    t.spotAngle = 179.999f;
                    t.range = 100f;
                    t.color = Color.white;
                }
            }
        }

        public void Charge(float charge)
        {
            _batteryLife += charge;
            if (_batteryLife > 4000f)
            {
                _batteryLife = 4000f;
            }
        }
    }
}