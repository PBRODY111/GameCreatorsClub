using UnityEngine;

namespace Scene6
{
    public class Engineer : MonoBehaviour
    {
        private static readonly int IsBegin = Animator.StringToHash("isBegin");
        private Animator _anim;

        private void Start()
        {
            _anim = GetComponent<Animator>();
            if (_anim == null)
                _anim = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E)) _anim.SetBool(IsBegin, true);
        }
    }
}