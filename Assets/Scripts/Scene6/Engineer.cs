using UnityEngine;

public class Engineer : MonoBehaviour
{
    private Animator _anim;
    private static readonly int IsBegin = Animator.StringToHash("isBegin");

    // Start is called before the first frame update
    private void Start()
    {
        _anim = GetComponent<Animator>();
        if (_anim == null)
            _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _anim.SetBool(IsBegin, true);
        }
    }
}