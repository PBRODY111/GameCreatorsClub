using System;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scene1.Safe
{
    public class Safe : InteractableObjectWithUI
    {
        protected Animator _safeAnimator;
        [SerializeField] protected AudioSource unlockAudio;
        [FormerlySerializedAs("_isUnlocked")] public bool isUnlocked;
        
        protected static readonly int Unlock = Animator.StringToHash("unlock");

        public void Awake()
        {
            _safeAnimator = GetComponentInChildren<Animator>();
        }

        protected void OpenSafe()
        {
            CloseUI();
            _safeAnimator.SetBool(Unlock, true);
            unlockAudio.Play();
            isUnlocked = true;
        }
    }
}