using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class InteractableObjectWithUI : InteractableObject
    {
        
        [SerializeField] protected GameObject ui;
        private bool uiCD;
        [SerializeField] protected PauseMenu pauseMenu;
        private TMP_InputField[] tmpInputFields;
        private InputField[] inputFields;


        public void Update()
        {
            if (ui.activeSelf && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)) && !uiCD) CloseUI();
            uiCD = false;
        }

        

        public new void OnMouseOver()
        {
            if (!ui.activeSelf)
                base.OnMouseOver();
            if (Input.GetKeyDown(KeyCode.E) && IsWithinReach() && !ui.activeSelf && !uiCD) OpenUI();
        }

        protected void OpenUI()
        {
            if(CanTurnOff()){
                pauseMenu.FreezeGame();
                ui.SetActive(true);
                uiCD = true;
            }
        }

        protected void CloseUI()
        {
            if(CanTurnOff()){
                ui.SetActive(false);
                pauseMenu.UnFreezeGame();
            }
        }

        private bool CanTurnOff(){
            tmpInputFields = FindObjectsOfType<TMP_InputField>();
            inputFields = FindObjectsOfType<InputField>();
            foreach (TMP_InputField tmpInputField in tmpInputFields)
            {
                if (tmpInputField.isFocused)
                {
                    return false;
                }
            }
            foreach (InputField inputField in inputFields)
            {
                if (inputField.isFocused)
                {
                    return false;
                }
            }
            return true;
        }
        
    }
}