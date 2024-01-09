using UnityEngine;
using UnityEngine.UIElements;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")] public float walkSpeed;

        public float sprintSpeed;

        public float groundDrag;

        public float jumpForce;
        public float jumpCooldown;
        public float airMultiplier;


        [Header("Keybinds")] public KeyCode jumpKey = KeyCode.Space;

        public KeyCode sprintKey = KeyCode.LeftShift;
        public KeyCode pauseKey = KeyCode.Escape;
        public KeyCode goDownKey = KeyCode.LeftShift;


        [Header("Ground Check")] public float playerHeight;

        public LayerMask ground;

        [Header("Misc")] public Transform orientation;

        public bool epicModeEnabled;
        private bool _grounded;

        private float _horizontalInput;

        private Vector3 _moveDirection;
        private float _moveSpeed;

        private Rigidbody _rb;
        private bool _readyToJump;
        private float _verticalInput;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.freezeRotation = true;

            _readyToJump = true;
        }

        private void Update()
        {
            SpeedControl();

            // handle drag
            if (_grounded)
                _rb.drag = groundDrag;
            else
                _rb.drag = 0;

            MyInput();
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(jumpKey) && _readyToJump && _grounded)
            {
                _readyToJump = false;

                Jump();

                Invoke(nameof(ResetJump), jumpCooldown);
            }

            _grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);
        }

        private void MyInput()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");

            if (Input.GetKey(sprintKey))
                _moveSpeed = sprintSpeed;
            else
                _moveSpeed = walkSpeed;

            MovePlayer();
        }

        private void MovePlayer()
        {
            // calculate movement direction
            _moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;

            // rotate physical body
            transform.rotation = orientation.rotation;

            if (epicModeEnabled)
                EpicModeMovement();
            else
                RegularMovement();
        }

        private void EpicModeMovement()
        {
            _moveSpeed = 1000f;
            _rb.AddForce(_moveDirection.normalized * (_moveSpeed * Time.deltaTime), ForceMode.VelocityChange);
            
            if (Input.GetKey(goDownKey))
            {
                if (!Input.GetKey(jumpKey))
                    _rb.AddForce(-transform.up * (_moveSpeed * Time.deltaTime), ForceMode.VelocityChange);
            }
            else if (Input.GetKey(jumpKey))
            {
                _rb.AddForce(transform.up * (_moveSpeed * Time.deltaTime), ForceMode.VelocityChange);
            }

            _rb.velocity = Vector3.zero;
        }

        private void RegularMovement()
        {
            var force = _moveDirection.normalized * (_moveSpeed * 500f * Time.deltaTime);
            if(!_grounded) force *= airMultiplier;
            
            _rb.AddForce(force, ForceMode.Force);
        }

        private void SpeedControl()
        {
            var flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > _moveSpeed)
            {
                var limitedVel = flatVel.normalized * _moveSpeed;
                _rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
            }
        }

        private void Jump()
        {
            // reset y velocity
            _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

            _rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        private void ResetJump()
        {
            _readyToJump = true;
        }
    }
}