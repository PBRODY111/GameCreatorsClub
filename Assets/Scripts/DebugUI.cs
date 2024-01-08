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
    [SerializeField] private Button fullbright;
    [SerializeField] private Button epic;
    [SerializeField] private Button fps;
    [SerializeField] private Light sun; 

    [SerializeField] private TMP_Text _fps;

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
        fullbright.onClick.AddListener(() =>
        {
            Player.Instance.GetComponent<HeadLamp>().Fullbright();
        });
        epic.onClick.AddListener(() => 
        {
            var player = Player.Instance;
            var rb = player.GetComponent<Rigidbody>();
            rb.useGravity = !rb.useGravity;
            var cc = player.transform.GetChild(0).GetComponent<CapsuleCollider>();
            cc.enabled = !cc.enabled;

            var pm = player.GetComponent<PlayerMovement>();
            pm.epicModeEnabled = !pm.epicModeEnabled;
        });
        fps.onClick.AddListener(() =>
        {
            _fps.gameObject.SetActive(!_fps.gameObject.activeSelf);
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
