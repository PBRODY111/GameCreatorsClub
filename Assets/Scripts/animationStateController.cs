using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    private Animator _animator;

    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        var isWalking = _animator.GetBool("isWalking");
        var isSprinting = _animator.GetBool("isSprinting");
        var isReversing = _animator.GetBool("isReversing");

        // walk forwards
        if (!isWalking && (Input.GetKey("w") || Input.GetKey("up")))
        {
            _animator.SetBool("isWalking", true);
        }

        if (isWalking && !(Input.GetKey("w") || Input.GetKey("up")))
        {
            _animator.SetBool("isWalking", false);
        }

        // walk backwards
        if (!isReversing && (Input.GetKey("s") || Input.GetKey("down")))
        {
            _animator.SetBool("isReversing", true);
        }

        if (isReversing && !(Input.GetKey("s") || Input.GetKey("down")))
        {
            _animator.SetBool("isReversing", false);
        }

        // sprint forwards
        if (isWalking && Input.GetKey("left shift"))
        {
            _animator.SetBool("isSprinting", true);
        }

        if (!isWalking && !Input.GetKey("left shift"))
        {
            _animator.SetBool("isSprinting", false);
        }
    }
}