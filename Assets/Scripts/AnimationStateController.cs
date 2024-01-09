using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    private Animator _animator;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsSprinting = Animator.StringToHash("isSprinting");
    private static readonly int IsReversing = Animator.StringToHash("isReversing");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleAnimationState("w", IsWalking, _animator.GetBool(IsWalking));
        HandleAnimationState("s", IsReversing, _animator.GetBool(IsReversing));
        HandleAnimationState("left shift", IsSprinting, _animator.GetBool(IsSprinting) && _animator.GetBool(IsWalking) && !Player.Player.Instance.EpicModeEnabled());
    }
    
    private void HandleAnimationState(string key, int hash, bool currentState)
    {
        if (!currentState && Input.GetKey(key))
        {
            _animator.SetBool(hash, true);
        }

        if (currentState && !Input.GetKey(key))
        {
            _animator.SetBool(hash, false);
        }
    }
}