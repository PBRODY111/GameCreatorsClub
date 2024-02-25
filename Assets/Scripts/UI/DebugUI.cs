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
        [SerializeField] private GameObject pauseMenu;

        [FormerlySerializedAs("_fps")] [SerializeField]
        private TMP_Text fps;

        private const float FPSUpdateInterval = 1.0f;

        private CapsuleCollider _collider;

        private float _fpsUpdateTimer;
        private HeadLamp _headLamp;
        private PlayerMovement _pm;

        public static int numDesks;

        private Rigidbody _rb;

        private int _fpsText;

        private KeyCode[] debugActivationSteps;
        private int debugActivate;
        private bool debugActive;

        private void Start()
        {
            _pm = Player.Player.Instance.GetComponent<PlayerMovement>();
            _collider = Player.Player.Instance.transform.GetChild(0).GetComponent<CapsuleCollider>();
            _headLamp = Player.Player.Instance.GetComponent<HeadLamp>();
            _rb = Player.Player.Instance.GetComponent<Rigidbody>();
            canvas.gameObject.SetActive(false);

            //FullBright
            fullbright.onClick.AddListener(() =>
            {
                _headLamp.Fullbright();
            });
            
            //Epic Mode
            epic.onClick.AddListener(() =>
            {
                _rb.useGravity = !_rb.useGravity;
                _collider.enabled = !_collider.enabled;
                _pm.epicModeEnabled = !_pm.epicModeEnabled;
                Player.Player.Instance.TogglerRespectablePostProcessing();
            });
            
            fpsButton.onClick.AddListener(() =>{
                fps.gameObject.SetActive(!fps.gameObject.activeSelf);});
            
            debugActivationSteps = new[]
            {
                KeyCode.UpArrow,
                KeyCode.UpArrow,
                KeyCode.DownArrow,
                KeyCode.DownArrow,
                KeyCode.LeftArrow,
                KeyCode.RightArrow,
                KeyCode.LeftArrow,
                KeyCode.RightArrow,
                KeyCode.A,
                KeyCode.B,
                KeyCode.S,
                KeyCode.T,
                KeyCode.A,
                KeyCode.R,
                KeyCode.T
            };
        }

        private void Update()
        {

            //Debug Activation
            if (Input.anyKeyDown && pauseMenu.activeSelf)
            {
                if (Input.GetKeyDown(debugActivationSteps[debugActivate]))
                    debugActivate++;
                else
                    debugActivate = 0;
                
                if (debugActivate == 15)
                {
                    debugActive = true;
                    ToggleDebug();
                }
            }
            
            
            if(Input.GetKeyDown(debugKey) && debugActive) ToggleDebug();

            //FPS Update Timer
            _fpsUpdateTimer += Time.deltaTime;
            if (_fpsUpdateTimer > FPSUpdateInterval)
            {
                _fpsText = Mathf.FloorToInt(1.0f / Time.deltaTime);
                _fpsUpdateTimer = 0f;
            }

            //Complete Level
            if (Input.GetKeyDown(KeyCode.F4) && Player.Player.Instance.EpicModeEnabled())
            {
                var vent = FindObjectOfType<Vent>();
                StartCoroutine(vent.EscapeFunc());
            }

            //Jumpscare
            if (Input.GetKeyDown(KeyCode.F5) && Player.Player.Instance.EpicModeEnabled())
            {
                var jumpscare = FindObjectOfType<Jumpscare>();
                StartCoroutine(jumpscare.JumpscareSequence());
            }
        }

        private void FixedUpdate()
        {
            fps.text = "FPS: " + _fpsText + "\nDesks: " + numDesks;
        }
        
        //Toggle Screen
        public void ToggleDebug()
        {
            debugActivate = 0;
            var o = canvas.gameObject;
            o.SetActive(!o.activeSelf);
            Cursor.lockState = o.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = o.activeSelf;
        }
    }
}