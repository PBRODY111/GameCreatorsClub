using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] public float walkSpeed;
    public float sprintSpeed;
    private float _moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    private bool _readyToJump;


    [Header("Keybinds")] public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode pauseKey = KeyCode.Escape;
    public KeyCode goDownKey = KeyCode.LeftShift;


    [Header("Ground Check")] public float playerHeight;
    public LayerMask ground;
    private bool _grounded;

    [Header("Misc")] public Transform orientation;

    public bool epicModeEnabled;

    private float _horizontalInput;
    private float _verticalInput;

    private Vector3 _moveDirection;

    private Rigidbody _rb;

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
        {
            _moveSpeed = 20f;
            transform.position += _moveDirection.normalized * _moveSpeed * Time.deltaTime;

            if (Input.GetKey(goDownKey))
            {
                if (!Input.GetKey(jumpKey))
                    transform.position -= transform.up * _moveSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(jumpKey))
                transform.position += transform.up * _moveSpeed * Time.deltaTime;

            _rb.velocity = Vector3.zero;
        }
        else
        {
            // on ground
            if (_grounded)
                _rb.AddForce(_moveDirection.normalized * _moveSpeed * 500f * Time.deltaTime, ForceMode.Force);

            // in air
            else
            {
                _rb.AddForce(_moveDirection.normalized * _moveSpeed * 500f * airMultiplier * Time.deltaTime,
                    ForceMode.Force);
            }
        }
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