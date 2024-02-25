using System;
using UnityEngine;

namespace UI
{
    public class InteractableObjectWithUI : InteractableObject
    {
        
        [SerializeField] protected GameObject ui;
        private bool uiCD;
        [SerializeField] protected PauseMenu pauseMenu;


        public void Update()
        {
            if (ui.activeSelf && Input.GetKeyDown(KeyCode.E) && !uiCD) CloseUI();
            uiCD = false;
        }

        

        public new void OnMouseOver()
        {
            if (!ui.activeSelf)
                base.OnMouseOver();
            if (Input.GetKeyDown(KeyCode.E) && IsWithinReach() && !ui.activeSelf && !uiCD) OpenUI();
        }

        private void OpenUI()
        {
            pauseMenu.FreezeGame();
            ui.SetActive(true);
            uiCD = true;
        }

        protected void CloseUI()
        {
            ui.SetActive(false);
            pauseMenu.UnFreezeGame();
        }
        
    }
}