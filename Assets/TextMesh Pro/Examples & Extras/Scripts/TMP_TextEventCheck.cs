using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace TextMesh_Pro.Examples___Extras.Scripts
{
    public class TMPTextEventCheck : MonoBehaviour
    {
        [FormerlySerializedAs("TextEventHandler")] public TMPTextEventHandler textEventHandler;

        private TMP_Text _mTextComponent;

        private void OnEnable()
        {
            if (textEventHandler != null)
            {
                // Get a reference to the text component
                _mTextComponent = textEventHandler.GetComponent<TMP_Text>();

                textEventHandler.OnCharacterSelection.AddListener(OnCharacterSelection);
                textEventHandler.OnSpriteSelection.AddListener(OnSpriteSelection);
                textEventHandler.OnWordSelection.AddListener(OnWordSelection);
                textEventHandler.OnLineSelection.AddListener(OnLineSelection);
                textEventHandler.OnLinkSelection.AddListener(OnLinkSelection);
            }
        }


        private void OnDisable()
        {
            if (textEventHandler != null)
            {
                textEventHandler.OnCharacterSelection.RemoveListener(OnCharacterSelection);
                textEventHandler.OnSpriteSelection.RemoveListener(OnSpriteSelection);
                textEventHandler.OnWordSelection.RemoveListener(OnWordSelection);
                textEventHandler.OnLineSelection.RemoveListener(OnLineSelection);
                textEventHandler.OnLinkSelection.RemoveListener(OnLinkSelection);
            }
        }


        private void OnCharacterSelection(char c, int index)
        {
            Debug.Log("Character [" + c + "] at Index: " + index + " has been selected.");
        }

        private void OnSpriteSelection(char c, int index)
        {
            Debug.Log("Sprite [" + c + "] at Index: " + index + " has been selected.");
        }

        private void OnWordSelection(string word, int firstCharacterIndex, int length)
        {
            Debug.Log("Word [" + word + "] with first character index of " + firstCharacterIndex + " and length of " +
                      length + " has been selected.");
        }

        private void OnLineSelection(string lineText, int firstCharacterIndex, int length)
        {
            Debug.Log("Line [" + lineText + "] with first character index of " + firstCharacterIndex +
                      " and length of " + length + " has been selected.");
        }

        private void OnLinkSelection(string linkID, string linkText, int linkIndex)
        {
            if (_mTextComponent != null)
            {
                var linkInfo = _mTextComponent.textInfo.linkInfo[linkIndex];
            }

            Debug.Log("Link Index: " + linkIndex + " with ID [" + linkID + "] and Text \"" + linkText +
                      "\" has been selected.");
        }
    }
}