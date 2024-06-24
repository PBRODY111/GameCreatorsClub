using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Steamworks;


namespace UI
{
    public class DebugUI : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private KeyCode debugKey = KeyCode.Escape;
        [SerializeField] private Button fullbright;
        [SerializeField] private Button epic;
        [SerializeField] private Button fpsButton;
        [SerializeField] private Button tinyMode;
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
        private static bool debugActive;

        private void Start()
        {
            Debug.Log("DebugUI Start");
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
                Player.Player.Instance.ToggleRespectablePostProcessing();
            });
            
            fpsButton.onClick.AddListener(() => fps.gameObject.SetActive(!fps.gameObject.activeSelf));
            
            tinyMode.onClick.AddListener(() => Player.Player.Instance.ToggleTinyMode());
            
            debugActivationSteps = new[]
            {
                KeyCode.UpArrow, KeyCode.UpArrow,
                KeyCode.DownArrow, KeyCode.DownArrow,
                KeyCode.LeftArrow, KeyCode.RightArrow,
                KeyCode.LeftArrow, KeyCode.RightArrow,
                KeyCode.A, KeyCode.B, // yes i know it's swapped, it's a feature now
                KeyCode.S, KeyCode.T, KeyCode.A, KeyCode.R, KeyCode.T
            };
        }
        
        public static bool DebugActive()
        {
            return debugActive;
        }

        private void Update()
        {
            //Debug Activation
            if (Input.anyKeyDown && pauseMenu.activeSelf && !debugActive)
            {
                if (Input.GetKeyDown(debugActivationSteps[debugActivate]))
                    debugActivate++;
                else
                    debugActivate = 0;
                
                if (debugActivate == 15)
                {
                    debugActive = true;
                    ToggleDebug();
                    //STEAM ACHIEVEMENTS
                    if(SteamManager.Initialized){
                        SteamUserStats.SetAchievement("EPIC_MODE");
                        SteamUserStats.StoreStats();
                    }
                }
            }
            
            if(!debugActive) return;
            
            if(Input.GetKeyDown(debugKey)) ToggleDebug();

            //FPS Update Timer
            _fpsUpdateTimer += Time.deltaTime;
            if (_fpsUpdateTimer > FPSUpdateInterval)
            {
                _fpsText = Mathf.FloorToInt(1.0f / Time.deltaTime);
                _fpsUpdateTimer = 0f;
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