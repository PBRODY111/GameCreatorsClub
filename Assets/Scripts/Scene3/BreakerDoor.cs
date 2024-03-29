using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakerDoor : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject intText;
    private Animator _doorAnim;
    private static readonly int IsOpen = Animator.StringToHash("isOpen");
    // Start is called before the first frame update
    private void Awake()
    {
        _doorAnim = GetComponent<Animator>();
        if (_doorAnim == null)
            _doorAnim = GetComponentInChildren<Animator>();
    }

    private void OnMouseExit()
    {
        intText.SetActive(false);
    }

    private void OnMouseOver()
    {
        intText.SetActive(IsWithinReach());
        if (Input.GetKeyDown(KeyCode.E) && IsWithinReach())
        {
            _doorAnim.SetBool(IsOpen, !_doorAnim.GetBool(IsOpen));
        }
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
