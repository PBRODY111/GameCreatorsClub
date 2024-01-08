using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLamp : MonoBehaviour
{
    private float _batteryLife = 3999f;
    public float batteryDrain = 100f;
    public int _lightStage = 4;
    [SerializeField] private GameObject _lightParent;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _batteryBarPrefab;
    private Light[] _lights;

    private bool _fullbright = false;

    private Color _color;

    void Start()
    {
        _lights = new Light[_lightParent.transform.childCount];
        for (int i = 0; i < _lights.Length; i++)
        {
            _lights[i] = _lightParent.transform.GetChild(i).GetComponent<Light>();
        }
        
        _color = _lights[0].color;

        Fullbright();
    }
    void Update()
    {
        if(_fullbright)
        {
            _batteryLife = 4000f;
        } else {
            if(_batteryLife > -1000f)
            _batteryLife -= Time.deltaTime * batteryDrain;
            _lightStage = Mathf.CeilToInt(_batteryLife / 1000f);
            _lights[0].intensity = 0.2f * (_lightStage+1);
            _lights[1].intensity = 0.07f * (_lightStage + 1);
            _lights[2].intensity = 0.07f * (_lightStage + 1);
            if (_canvas.transform.GetChild(0).childCount != _lightStage + 1)
            {
                int temp = _canvas.transform.GetChild(0).childCount;
                for (int i = 0; i < (temp); i++)
                {
                    Destroy(_canvas.transform.GetChild(0).GetChild(i).gameObject);
                }
            for (int i = 0; i < (_lightStage+1); i++)
                {
                    Instantiate(_batteryBarPrefab, _canvas.transform.GetChild(0));
                }
            }
        }
    }

    public void Fullbright()
    {
        if(_fullbright)
        {
            _fullbright = false;
            for (int i = 0; i < _lights.Length; i++)
            {
                _lights[i].intensity = 0.2f * (_lightStage + 1);
                _lights[i].spotAngle = 60f;
                _lights[i].range = 10f;
                _lights[i].color = _color;
            }
        }
        else
        {
            _fullbright = true;
            for (int i = 0; i < _lights.Length; i++)
            {
                _lights[i].intensity = 0.6f;
                _lights[i].spotAngle = 179.999f;
                _lights[i].range = 100f;
                _lights[i].color = Color.white;
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
