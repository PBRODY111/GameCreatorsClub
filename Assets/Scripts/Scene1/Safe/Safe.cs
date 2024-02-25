using System;
using UI;
using UnityEngine;

namespace Scene1.Safe
{
    public class Safe : MonoBehaviour
    {
        [SerializeField] protected GameObject intText;
        [SerializeField] protected float reach;
        [SerializeField] protected GameObject ui;
        protected Animator _safeAnimator;
        [SerializeField] protected AudioSource unlockAudio;
        public bool _isUnlocked;

        [SerializeField] private PauseMenu pauseMenu;
        
        protected static readonly int Unlock = Animator.StringToHash("unlock");

        public void Awake()
        {
            _safeAnimator = GetComponentInChildren<Animator>();
        }

        public void Update()
        {
            if (ui.activeSelf) HandleEscapeKey();
        }
        
        public void OnMouseExit()
        {
            if (!ui.activeSelf) intText.SetActive(false);
        }
        
        public void OnMouseOver()
        {
            if (!ui.activeSelf && !_isUnlocked)
                intText.SetActive(IsWithinReach());
            if (Input.GetKeyDown(KeyCode.E) && !_isUnlocked && IsWithinReach())
            {
                intText.SetActive(false);
                ui.SetActive(true);
                PauseMenu.IsPaused = true;
                Player.Player.Instance.DisableMovement();
                Player.Player.Instance.UnlockCursor();
            }
        }
        
        protected bool IsWithinReach()
        {
            return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
        }
        
        protected void HandleEscapeKey()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ui.SetActive(false);
                PauseMenu.IsPaused = false;
                Player.Player.Instance.LockCursor();
                Player.Player.Instance.EnableMovement();
            }
        }
    }
}