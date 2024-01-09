using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class DebugUI : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private KeyCode debugKey = KeyCode.F3;
        [SerializeField] private Button fullbright;
        [SerializeField] private Button epic;
        [SerializeField] private Button fpsButton;
        [SerializeField] private Light sun;

        [FormerlySerializedAs("_fps")]
        [SerializeField]
        private TMP_Text fps;

        private const float FPSUpdateInterval = 1.0f;

        private CapsuleCollider _collider;

        private float _fpsUpdateTimer;
        private HeadLamp _headLamp;
        private PlayerMovement _pm;

        private Rigidbody _rb;

        // Start is called before the first frame update
        private void Start()
        {
            _pm = Player.Player.Instance.GetComponent<PlayerMovement>();
            _collider = Player.Player.Instance.transform.GetChild(0).GetComponent<CapsuleCollider>();
            _headLamp = Player.Player.Instance.GetComponent<HeadLamp>();
            _rb = Player.Player.Instance.GetComponent<Rigidbody>();
            canvas.gameObject.SetActive(false);
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(debugKey))
            {
                var o = canvas.gameObject;
                o.SetActive(!o.activeSelf);
                Cursor.lockState = o.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
                Cursor.visible = o.activeSelf;
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
            if (_fpsUpdateTimer < FPSUpdateInterval) return;

            UpdateFps();
            _fpsUpdateTimer = 0f; // Reset the timer
        }

        private void UpdateFps()
        {
            var roundedFps = Mathf.FloorToInt(1.0f / Time.deltaTime);
            fps.text = "FPS: " + roundedFps;
        }
    }
}