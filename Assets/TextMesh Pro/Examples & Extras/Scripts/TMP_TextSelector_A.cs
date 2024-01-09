using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TextMesh_Pro.Examples___Extras.Scripts
{
    public class TMPTextSelectorA : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private TextMeshPro _mTextMeshPro;

        private Camera _mCamera;

        private bool _mIsHoveringObject;
        private int _mSelectedLink = -1;
        private int _mLastCharIndex = -1;
        private int _mLastWordIndex = -1;

        private void Awake()
        {
            _mTextMeshPro = gameObject.GetComponent<TextMeshPro>();
            _mCamera = Camera.main;

            // Force generation of the text object so we have valid data to work with. This is needed since LateUpdate() will be called before the text object has a chance to generated when entering play mode.
            _mTextMeshPro.ForceMeshUpdate();
        }


        private void LateUpdate()
        {
            _mIsHoveringObject = false;

            if (TMP_TextUtilities.IsIntersectingRectTransform(_mTextMeshPro.rectTransform, Input.mousePosition,
                    Camera.main))
            {
                _mIsHoveringObject = true;
            }

            if (_mIsHoveringObject)
            {
                #region Example of Character Selection

                var charIndex =
                    TMP_TextUtilities.FindIntersectingCharacter(_mTextMeshPro, Input.mousePosition, Camera.main, true);
                if (charIndex != -1 && charIndex != _mLastCharIndex &&
                    (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
                {
                    //Debug.Log("[" + m_TextMeshPro.textInfo.characterInfo[charIndex].character + "] has been selected.");

                    _mLastCharIndex = charIndex;

                    var meshIndex = _mTextMeshPro.textInfo.characterInfo[charIndex].materialReferenceIndex;

                    var vertexIndex = _mTextMeshPro.textInfo.characterInfo[charIndex].vertexIndex;

                    var c = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255),
                        (byte)Random.Range(0, 255), 255);

                    var vertexColors = _mTextMeshPro.textInfo.meshInfo[meshIndex].colors32;

                    vertexColors[vertexIndex + 0] = c;
                    vertexColors[vertexIndex + 1] = c;
                    vertexColors[vertexIndex + 2] = c;
                    vertexColors[vertexIndex + 3] = c;

                    //m_TextMeshPro.mesh.colors32 = vertexColors;
                    _mTextMeshPro.textInfo.meshInfo[meshIndex].mesh.colors32 = vertexColors;
                }

                #endregion

                #region Example of Link Handling

                // Check if mouse intersects with any links.
                var linkIndex = TMP_TextUtilities.FindIntersectingLink(_mTextMeshPro, Input.mousePosition, _mCamera);

                // Clear previous link selection if one existed.
                if ((linkIndex == -1 && _mSelectedLink != -1) || linkIndex != _mSelectedLink)
                {
                    //m_TextPopup_RectTransform.gameObject.SetActive(false);
                    _mSelectedLink = -1;
                }

                // Handle new Link selection.
                if (linkIndex != -1 && linkIndex != _mSelectedLink)
                {
                    _mSelectedLink = linkIndex;

                    var linkInfo = _mTextMeshPro.textInfo.linkInfo[linkIndex];

                    // The following provides an example of how to access the link properties.
                    //Debug.Log("Link ID: \"" + linkInfo.GetLinkID() + "\"   Link Text: \"" + linkInfo.GetLinkText() + "\""); // Example of how to retrieve the Link ID and Link Text.

                    Vector3 worldPointInRectangle;

                    RectTransformUtility.ScreenPointToWorldPointInRectangle(_mTextMeshPro.rectTransform,
                        Input.mousePosition, _mCamera, out worldPointInRectangle);

                    switch (linkInfo.GetLinkID())
                    {
                        case "id_01": // 100041637: // id_01
                            //m_TextPopup_RectTransform.position = worldPointInRectangle;
                            //m_TextPopup_RectTransform.gameObject.SetActive(true);
                            //m_TextPopup_TMPComponent.text = k_LinkText + " ID 01";
                            break;
                        case "id_02": // 100041638: // id_02
                            //m_TextPopup_RectTransform.position = worldPointInRectangle;
                            //m_TextPopup_RectTransform.gameObject.SetActive(true);
                            //m_TextPopup_TMPComponent.text = k_LinkText + " ID 02";
                            break;
                    }
                }

                #endregion


                #region Example of Word Selection

                // Check if Mouse intersects any words and if so assign a random color to that word.
                var wordIndex = TMP_TextUtilities.FindIntersectingWord(_mTextMeshPro, Input.mousePosition, Camera.main);
                if (wordIndex != -1 && wordIndex != _mLastWordIndex)
                {
                    _mLastWordIndex = wordIndex;

                    var wInfo = _mTextMeshPro.textInfo.wordInfo[wordIndex];

                    var wordPos = _mTextMeshPro.transform.TransformPoint(_mTextMeshPro.textInfo
                        .characterInfo[wInfo.firstCharacterIndex].bottomLeft);
                    wordPos = Camera.main.WorldToScreenPoint(wordPos);

                    //Debug.Log("Mouse Position: " + Input.mousePosition.ToString("f3") + "  Word Position: " + wordPOS.ToString("f3"));

                    var vertexColors = _mTextMeshPro.textInfo.meshInfo[0].colors32;

                    var c = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255),
                        (byte)Random.Range(0, 255), 255);
                    for (var i = 0; i < wInfo.characterCount; i++)
                    {
                        var vertexIndex = _mTextMeshPro.textInfo.characterInfo[wInfo.firstCharacterIndex + i]
                            .vertexIndex;

                        vertexColors[vertexIndex + 0] = c;
                        vertexColors[vertexIndex + 1] = c;
                        vertexColors[vertexIndex + 2] = c;
                        vertexColors[vertexIndex + 3] = c;
                    }

                    _mTextMeshPro.mesh.colors32 = vertexColors;
                }

                #endregion
            }
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("OnPointerEnter()");
            _mIsHoveringObject = true;
        }


        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("OnPointerExit()");
            _mIsHoveringObject = false;
        }
    }
}