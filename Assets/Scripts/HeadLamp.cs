using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLamp : MonoBehaviour
{
    private float _batteryLife = 3999f;
    public float batteryDrain = 100f;
    private int _lightStage = 4;
    [SerializeField] private GameObject _lightParent;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _batteryBarPrefab;
    private Light[] _lights;

    void Start()
    {
        _lights = new Light[_lightParent.transform.childCount];
        for (int i = 0; i < _lights.Length; i++)
        {
            _lights[i] = _lightParent.transform.GetChild(i).GetComponent<Light>();
        }
    }
    void Update()
    {
            _batteryLife -= Time.deltaTime * batteryDrain;
            _lightStage = Mathf.FloorToInt(_batteryLife / 1000) + 1;
            _lights[0].intensity = 0.5f * _lightStage;
            _lights[1].intensity = 0.1f * _lightStage;
            _lights[2].intensity = 0.1f * _lightStage;
            if (_canvas.transform.GetChild(0).childCount != _lightStage + 1)
            {
                int temp = _canvas.transform.GetChild(0).childCount;
                for (int i = 0; i < (temp); i++)
                {
                    Destroy(_canvas.transform.GetChild(0).GetChild(i).gameObject);
                }
                Debug.Log("Destroyed+"+temp+"Adding : "+(_lightStage+1));
            for (int i = 0; i < (_lightStage + 1); i++)
                {
                    Instantiate(_batteryBarPrefab, _canvas.transform.GetChild(0));
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
