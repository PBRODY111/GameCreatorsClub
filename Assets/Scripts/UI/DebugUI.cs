using Player;
using Scene1;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace UI
{
    public class DebugUI : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private KeyCode debugKey = KeyCode.Escape;
        [SerializeField] private Button fullbright;
        [SerializeField] private Button epic;
        [SerializeField] private Button fpsButton;
        [SerializeField] private Light sun;
        [SerializeField] private GameObject pauseMenu;

        [FormerlySerializedAs("_fps")] [SerializeField]
        private TMP_Text fps;

        private const float FPSUpdateInterval = 1.0f;

        private CapsuleCollider _collider;

        private float _fpsUpdateTimer;
        private HeadLamp _headLamp;
        private PlayerMovement _pm;

        public static int NumDesks = 0;

        private Rigidbody _rb;

        private int _fpsText;

        private int konami = 0;

        private void Start()
        {
            _pm = Player.Player.Instance.GetComponent<PlayerMovement>();
            _collider = Player.Player.Instance.transform.GetChild(0).GetComponent<CapsuleCollider>();
            _headLamp = Player.Player.Instance.GetComponent<HeadLamp>();
            _rb = Player.Player.Instance.GetComponent<Rigidbody>();
            canvas.gameObject.SetActive(false);

            fullbright.onClick.AddListener(() => _headLamp.Fullbright());
            epic.onClick.AddListener(() =>
            {
                _rb.useGravity = !_rb.useGravity;
                _collider.enabled = !_collider.enabled;
                _pm.epicModeEnabled = !_pm.epicModeEnabled;
            });
            fpsButton.onClick.AddListener(() => fps.gameObject.SetActive(!fps.gameObject.activeSelf));
        }

        private void Update()
        {

            if (Input.anyKeyDown && pauseMenu.activeSelf)
            {
                if (Input.GetKeyDown(Konami(konami)))
                    konami++;
                else
                    konami = 0;
            }
            if(konami==15 || (Input.GetKeyDown(debugKey) && canvas.gameObject.activeSelf))
            {
                konami = 0;
                var o = canvas.gameObject;
                o.SetActive(!o.activeSelf);
                Cursor.lockState = o.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
                Cursor.visible = o.activeSelf;
            }

            _fpsUpdateTimer += Time.deltaTime;
            if (_fpsUpdateTimer > FPSUpdateInterval)
            {
                _fpsText = Mathf.FloorToInt(1.0f / Time.deltaTime);
                _fpsUpdateTimer = 0f;
            }

            if (Input.GetKeyDown(KeyCode.F4) && Player.Player.Instance.EpicModeEnabled())
            {
                var vent = FindObjectOfType<Vent>();
                StartCoroutine(vent.EscapeFunc());
            }

            if (Input.GetKeyDown(KeyCode.F5) && Player.Player.Instance.EpicModeEnabled())
            {
                var jumpscare = FindObjectOfType<Jumpscare>();
                StartCoroutine(jumpscare.JumpscareSequence());
            }
        }

        private void FixedUpdate()
        {
            fps.text = "FPS: " + _fpsText + "\nDesks: " + NumDesks;
        }
        
            

        private KeyCode Konami(int konami)
        {
            Debug.Log(":"+konami);
            switch (this.konami)
            {
                case 0:
                    return KeyCode.UpArrow;
                case 1:
                    return KeyCode.UpArrow;
                case 2:
                    return KeyCode.DownArrow;
                case 3:
                    return KeyCode.DownArrow;
                case 4:
                    return KeyCode.LeftArrow;
                case 5:
                    return KeyCode.RightArrow;
                case 6:
                    return KeyCode.LeftArrow;
                case 7:
                    return KeyCode.RightArrow;
                case 8:
                    return KeyCode.A;
                case 9:
                    return KeyCode.B;
                case 10:
                    return KeyCode.S;
                case 11:
                    return KeyCode.T;
                case 12:
                    return KeyCode.A;
                case 13:
                    return KeyCode.R;
                case 14:
                    return KeyCode.T;
                default:
                    return KeyCode.Alpha0;
            }
        }
    }
}