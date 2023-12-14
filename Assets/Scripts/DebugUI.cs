using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private KeyCode debugKey = KeyCode.F3;
    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    [SerializeField] private TMP_Text _fps;
    [SerializeField] private Light sun; 
    private float sunIntensity = 1.0f;

    float fpsUpdateTimer = 0f;
    float fpsUpdateInterval = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        canvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(debugKey))
        {
            canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
            Cursor.lockState = canvas.gameObject.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = canvas.gameObject.activeSelf;
        }
        button1.onClick.AddListener(() =>
        {
            sun.intensity += sunIntensity;
            sunIntensity = sun.intensity - sunIntensity;
            sun.intensity -= sunIntensity;
            Debug.Log("Sun clicked. Intensity set to:"+sun.intensity);
        });
        button2.onClick.AddListener(() => 
        {
            Player.Instance.GetComponent<Rigidbody>().useGravity = !Player.Instance.GetComponent<Rigidbody>().useGravity;
            Player.Instance.transform.GetChild(0).GetComponent<CapsuleCollider>().enabled = !Player.Instance.transform.GetChild(0).GetComponent<CapsuleCollider>().enabled;
        });
        fpsUpdateTimer += Time.deltaTime;
        if (fpsUpdateTimer >= fpsUpdateInterval)
        {
            UpdateFps();
            fpsUpdateTimer = 0f; // Reset the timer
        }
    }

    void UpdateFps()
    {
        int roundedFps = Mathf.FloorToInt(1.0f / Time.deltaTime);
        _fps.text = "FPS: " + roundedFps.ToString();
    }
}
