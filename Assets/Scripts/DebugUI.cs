using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private KeyCode debugKey = KeyCode.F3;
    [SerializeField] private Button fullbright;
    [SerializeField] private Button epic;
    [SerializeField] private Button fpsButton;
    [SerializeField] private Light sun;

    [FormerlySerializedAs("_fps")] [SerializeField] private TMP_Text fps;

    private float _fpsUpdateTimer;
    private float _fpsUpdateInterval = 1.0f;
    
    private Rigidbody _rb;
    private HeadLamp _headLamp;
    private CapsuleCollider _collider;
    private PlayerMovement _pm;

    // Start is called before the first frame update
    private void Start()
    {
        _pm = Player.Instance.GetComponent<PlayerMovement>();
        _collider = Player.Instance.transform.GetChild(0).GetComponent<CapsuleCollider>();
        _headLamp = Player.Instance.GetComponent<HeadLamp>();
        _rb = Player.Instance.GetComponent<Rigidbody>();
        canvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(debugKey))
        {
            canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
            Cursor.lockState = canvas.gameObject.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = canvas.gameObject.activeSelf;
        }

        fullbright.onClick.AddListener(() => _headLamp.Fullbright());
        epic.onClick.AddListener(() =>
        {
            _rb.useGravity = !_rb.useGravity;
            _collider.enabled = !_collider.enabled;
            _pm.epicModeEnabled = !_pm.epicModeEnabled;
        });
        fpsButton.onClick.AddListener(() => fps.gameObject.SetActive(!fps.gameObject.activeSelf));

        _fpsUpdateTimer += Time.deltaTime;
        if (_fpsUpdateTimer < _fpsUpdateInterval) return;
        
        UpdateFps();
        _fpsUpdateTimer = 0f; // Reset the timer
    }

    private void UpdateFps()
    {
        var roundedFps = Mathf.FloorToInt(1.0f / Time.deltaTime);
        fps.text = "FPS: " + roundedFps;
    }
}