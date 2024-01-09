using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.Serialization;


namespace TMPro.Examples
{
    public class TMPTextInfoDebugTool : MonoBehaviour
    {
        // Since this script is used for debugging, we exclude it from builds.
        // TODO: Rework this script to make it into an editor utility.
#if UNITY_EDITOR
        [FormerlySerializedAs("ShowCharacters")] public bool showCharacters;
        [FormerlySerializedAs("ShowWords")] public bool showWords;
        [FormerlySerializedAs("ShowLinks")] public bool showLinks;
        [FormerlySerializedAs("ShowLines")] public bool showLines;
        [FormerlySerializedAs("ShowMeshBounds")] public bool showMeshBounds;
        [FormerlySerializedAs("ShowTextBounds")] public bool showTextBounds;
        [FormerlySerializedAs("ObjectStats")] [Space(10)] [TextArea(2, 2)] public string objectStats;

        [FormerlySerializedAs("m_TextComponent")] [SerializeField] private TMP_Text mTextComponent;

        private Transform _mTransform;
        private TMP_TextInfo _mTextInfo;

        private float _mScaleMultiplier;
        private float _mHandleSize;


        private void OnDrawGizmos()
        {
            if (mTextComponent == null)
            {
                mTextComponent = GetComponent<TMP_Text>();

                if (mTextComponent == null)
                    return;
            }

            _mTransform = mTextComponent.transform;

            // Get a reference to the text object's textInfo
            _mTextInfo = mTextComponent.textInfo;

            // Update Text Statistics
            objectStats = "Characters: " + _mTextInfo.characterCount + "   Words: " + _mTextInfo.wordCount +
                          "   Spaces: " + _mTextInfo.spaceCount + "   Sprites: " + _mTextInfo.spriteCount +
                          "   Links: " + _mTextInfo.linkCount
                          + "\nLines: " + _mTextInfo.lineCount + "   Pages: " + _mTextInfo.pageCount;

            // Get the handle size for drawing the various
            _mScaleMultiplier = mTextComponent.GetType() == typeof(TextMeshPro) ? 1 : 0.1f;
            _mHandleSize = HandleUtility.GetHandleSize(_mTransform.position) * _mScaleMultiplier;

            // Draw line metrics

            #region Draw Lines

            if (showLines)
                DrawLineBounds();

            #endregion

            // Draw word metrics

            #region Draw Words

            if (showWords)
                DrawWordBounds();

            #endregion

            // Draw character metrics

            #region Draw Characters

            if (showCharacters)
                DrawCharactersBounds();

            #endregion

            // Draw Quads around each of the words

            #region Draw Links

            if (showLinks)
                DrawLinkBounds();

            #endregion

            // Draw Quad around the bounds of the text

            #region Draw Bounds

            if (showMeshBounds)
                DrawBounds();

            #endregion

            // Draw Quad around the rendered region of the text.

            #region Draw Text Bounds

            if (showTextBounds)
                DrawTextBounds();

            #endregion
        }


        /// <summary>
        /// Method to draw a rectangle around each character.
        /// </summary>
        /// <param name="text"></param>
        private void DrawCharactersBounds()
        {
            var characterCount = _mTextInfo.characterCount;

            for (var i = 0; i < characterCount; i++)
            {
                // Draw visible as well as invisible characters
                var characterInfo = _mTextInfo.characterInfo[i];

                var isCharacterVisible = i < mTextComponent.maxVisibleCharacters &&
                                         characterInfo.lineNumber < mTextComponent.maxVisibleLines &&
                                         i >= mTextComponent.firstVisibleCharacter;

                if (mTextComponent.overflowMode == TextOverflowModes.Page)
                    isCharacterVisible = isCharacterVisible &&
                                         characterInfo.pageNumber + 1 == mTextComponent.pageToDisplay;

                if (!isCharacterVisible)
                    continue;

                float dottedLineSize = 6;

                // Get Bottom Left and Top Right position of the current character
                var bottomLeft = _mTransform.TransformPoint(characterInfo.bottomLeft);
                var topLeft =
                    _mTransform.TransformPoint(new Vector3(characterInfo.topLeft.x, characterInfo.topLeft.y, 0));
                var topRight = _mTransform.TransformPoint(characterInfo.topRight);
                var bottomRight =
                    _mTransform.TransformPoint(new Vector3(characterInfo.bottomRight.x, characterInfo.bottomRight.y,
                        0));

                // Draw character bounds
                if (characterInfo.isVisible)
                {
                    var color = Color.green;
                    DrawDottedRectangle(bottomLeft, topRight, color);
                }
                else
                {
                    var color = Color.grey;

                    var whiteSpaceAdvance = Math.Abs(characterInfo.origin - characterInfo.xAdvance) > 0.01f
                        ? characterInfo.xAdvance
                        : characterInfo.origin + (characterInfo.ascender - characterInfo.descender) * 0.03f;
                    DrawDottedRectangle(
                        _mTransform.TransformPoint(new Vector3(characterInfo.origin, characterInfo.descender, 0)),
                        _mTransform.TransformPoint(new Vector3(whiteSpaceAdvance, characterInfo.ascender, 0)), color,
                        4);
                }

                var origin = characterInfo.origin;
                var advance = characterInfo.xAdvance;
                var ascentline = characterInfo.ascender;
                var baseline = characterInfo.baseLine;
                var descentline = characterInfo.descender;

                //Draw Ascent line
                var ascentlineStart = _mTransform.TransformPoint(new Vector3(origin, ascentline, 0));
                var ascentlineEnd = _mTransform.TransformPoint(new Vector3(advance, ascentline, 0));

                Handles.color = Color.cyan;
                Handles.DrawDottedLine(ascentlineStart, ascentlineEnd, dottedLineSize);

                // Draw Cap Height & Mean line
                var capline = characterInfo.fontAsset == null
                    ? 0
                    : baseline + characterInfo.fontAsset.faceInfo.capLine * characterInfo.scale;
                var capHeightStart =
                    new Vector3(topLeft.x, _mTransform.TransformPoint(new Vector3(0, capline, 0)).y, 0);
                var capHeightEnd =
                    new Vector3(topRight.x, _mTransform.TransformPoint(new Vector3(0, capline, 0)).y, 0);

                var meanline = characterInfo.fontAsset == null
                    ? 0
                    : baseline + characterInfo.fontAsset.faceInfo.meanLine * characterInfo.scale;
                var meanlineStart =
                    new Vector3(topLeft.x, _mTransform.TransformPoint(new Vector3(0, meanline, 0)).y, 0);
                var meanlineEnd = new Vector3(topRight.x, _mTransform.TransformPoint(new Vector3(0, meanline, 0)).y,
                    0);

                if (characterInfo.isVisible)
                {
                    // Cap line
                    Handles.color = Color.cyan;
                    Handles.DrawDottedLine(capHeightStart, capHeightEnd, dottedLineSize);

                    // Mean line
                    Handles.color = Color.cyan;
                    Handles.DrawDottedLine(meanlineStart, meanlineEnd, dottedLineSize);
                }

                //Draw Base line
                var baselineStart = _mTransform.TransformPoint(new Vector3(origin, baseline, 0));
                var baselineEnd = _mTransform.TransformPoint(new Vector3(advance, baseline, 0));

                Handles.color = Color.cyan;
                Handles.DrawDottedLine(baselineStart, baselineEnd, dottedLineSize);

                //Draw Descent line
                var descentlineStart = _mTransform.TransformPoint(new Vector3(origin, descentline, 0));
                var descentlineEnd = _mTransform.TransformPoint(new Vector3(advance, descentline, 0));

                Handles.color = Color.cyan;
                Handles.DrawDottedLine(descentlineStart, descentlineEnd, dottedLineSize);

                // Draw Origin
                var originPosition = _mTransform.TransformPoint(new Vector3(origin, baseline, 0));
                DrawCrosshair(originPosition, 0.05f / _mScaleMultiplier, Color.cyan);

                // Draw Horizontal Advance
                var advancePosition = _mTransform.TransformPoint(new Vector3(advance, baseline, 0));
                DrawSquare(advancePosition, 0.025f / _mScaleMultiplier, Color.yellow);
                DrawCrosshair(advancePosition, 0.0125f / _mScaleMultiplier, Color.yellow);

                // Draw text labels for metrics
                if (_mHandleSize < 0.5f)
                {
                    var style = new GUIStyle(GUI.skin.GetStyle("Label"));
                    style.normal.textColor = new Color(0.6f, 0.6f, 0.6f, 1.0f);
                    style.fontSize = 12;
                    style.fixedWidth = 200;
                    style.fixedHeight = 20;

                    Vector3 labelPosition;
                    var center = (origin + advance) / 2;

                    //float baselineMetrics = 0;
                    //float ascentlineMetrics = ascentline - baseline;
                    //float caplineMetrics = capline - baseline;
                    //float meanlineMetrics = meanline - baseline;
                    //float descentlineMetrics = descentline - baseline;

                    // Ascent Line
                    labelPosition = _mTransform.TransformPoint(new Vector3(center, ascentline, 0));
                    style.alignment = TextAnchor.UpperCenter;
                    Handles.Label(labelPosition, "Ascent Line", style);
                    //Handles.Label(labelPosition, "Ascent Line (" + ascentlineMetrics.ToString("f3") + ")" , style);

                    // Base Line
                    labelPosition = _mTransform.TransformPoint(new Vector3(center, baseline, 0));
                    Handles.Label(labelPosition, "Base Line", style);
                    //Handles.Label(labelPosition, "Base Line (" + baselineMetrics.ToString("f3") + ")" , style);

                    // Descent line
                    labelPosition = _mTransform.TransformPoint(new Vector3(center, descentline, 0));
                    Handles.Label(labelPosition, "Descent Line", style);
                    //Handles.Label(labelPosition, "Descent Line (" + descentlineMetrics.ToString("f3") + ")" , style);

                    if (characterInfo.isVisible)
                    {
                        // Cap Line
                        labelPosition = _mTransform.TransformPoint(new Vector3(center, capline, 0));
                        style.alignment = TextAnchor.UpperCenter;
                        Handles.Label(labelPosition, "Cap Line", style);
                        //Handles.Label(labelPosition, "Cap Line (" + caplineMetrics.ToString("f3") + ")" , style);

                        // Mean Line
                        labelPosition = _mTransform.TransformPoint(new Vector3(center, meanline, 0));
                        style.alignment = TextAnchor.UpperCenter;
                        Handles.Label(labelPosition, "Mean Line", style);
                        //Handles.Label(labelPosition, "Mean Line (" + ascentlineMetrics.ToString("f3") + ")" , style);

                        // Origin
                        labelPosition = _mTransform.TransformPoint(new Vector3(origin, baseline, 0));
                        style.alignment = TextAnchor.UpperRight;
                        Handles.Label(labelPosition, "Origin ", style);

                        // Advance
                        labelPosition = _mTransform.TransformPoint(new Vector3(advance, baseline, 0));
                        style.alignment = TextAnchor.UpperLeft;
                        Handles.Label(labelPosition, "  Advance", style);
                    }
                }
            }
        }


        /// <summary>
        /// Method to draw rectangles around each word of the text.
        /// </summary>
        /// <param name="text"></param>
        private void DrawWordBounds()
        {
            for (var i = 0; i < _mTextInfo.wordCount; i++)
            {
                var wInfo = _mTextInfo.wordInfo[i];

                var isBeginRegion = false;

                var bottomLeft = Vector3.zero;
                var topLeft = Vector3.zero;
                var bottomRight = Vector3.zero;
                var topRight = Vector3.zero;

                var maxAscender = -Mathf.Infinity;
                var minDescender = Mathf.Infinity;

                var wordColor = Color.green;

                // Iterate through each character of the word
                for (var j = 0; j < wInfo.characterCount; j++)
                {
                    var characterIndex = wInfo.firstCharacterIndex + j;
                    var currentCharInfo = _mTextInfo.characterInfo[characterIndex];
                    var currentLine = currentCharInfo.lineNumber;

                    var isCharacterVisible = characterIndex > mTextComponent.maxVisibleCharacters ||
                                             currentCharInfo.lineNumber > mTextComponent.maxVisibleLines ||
                                             (mTextComponent.overflowMode == TextOverflowModes.Page &&
                                              currentCharInfo.pageNumber + 1 != mTextComponent.pageToDisplay)
                        ? false
                        : true;

                    // Track Max Ascender and Min Descender
                    maxAscender = Mathf.Max(maxAscender, currentCharInfo.ascender);
                    minDescender = Mathf.Min(minDescender, currentCharInfo.descender);

                    if (isBeginRegion == false && isCharacterVisible)
                    {
                        isBeginRegion = true;

                        bottomLeft = new Vector3(currentCharInfo.bottomLeft.x, currentCharInfo.descender, 0);
                        topLeft = new Vector3(currentCharInfo.bottomLeft.x, currentCharInfo.ascender, 0);

                        //Debug.Log("Start Word Region at [" + currentCharInfo.character + "]");

                        // If Word is one character
                        if (wInfo.characterCount == 1)
                        {
                            isBeginRegion = false;

                            topLeft = _mTransform.TransformPoint(new Vector3(topLeft.x, maxAscender, 0));
                            bottomLeft = _mTransform.TransformPoint(new Vector3(bottomLeft.x, minDescender, 0));
                            bottomRight =
                                _mTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, minDescender, 0));
                            topRight = _mTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, maxAscender,
                                0));

                            // Draw Region
                            DrawRectangle(bottomLeft, topLeft, topRight, bottomRight, wordColor);

                            //Debug.Log("End Word Region at [" + currentCharInfo.character + "]");
                        }
                    }

                    // Last Character of Word
                    if (isBeginRegion && j == wInfo.characterCount - 1)
                    {
                        isBeginRegion = false;

                        topLeft = _mTransform.TransformPoint(new Vector3(topLeft.x, maxAscender, 0));
                        bottomLeft = _mTransform.TransformPoint(new Vector3(bottomLeft.x, minDescender, 0));
                        bottomRight =
                            _mTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, minDescender, 0));
                        topRight = _mTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, maxAscender, 0));

                        // Draw Region
                        DrawRectangle(bottomLeft, topLeft, topRight, bottomRight, wordColor);

                        //Debug.Log("End Word Region at [" + currentCharInfo.character + "]");
                    }
                    // If Word is split on more than one line.
                    else if (isBeginRegion && currentLine != _mTextInfo.characterInfo[characterIndex + 1].lineNumber)
                    {
                        isBeginRegion = false;

                        topLeft = _mTransform.TransformPoint(new Vector3(topLeft.x, maxAscender, 0));
                        bottomLeft = _mTransform.TransformPoint(new Vector3(bottomLeft.x, minDescender, 0));
                        bottomRight =
                            _mTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, minDescender, 0));
                        topRight = _mTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, maxAscender, 0));

                        // Draw Region
                        DrawRectangle(bottomLeft, topLeft, topRight, bottomRight, wordColor);
                        //Debug.Log("End Word Region at [" + currentCharInfo.character + "]");
                        maxAscender = -Mathf.Infinity;
                        minDescender = Mathf.Infinity;
                    }
                }

                //Debug.Log(wInfo.GetWord(m_TextMeshPro.textInfo.characterInfo));
            }
        }


        /// <summary>
        /// Draw rectangle around each of the links contained in the text.
        /// </summary>
        /// <param name="text"></param>
        private void DrawLinkBounds()
        {
            var textInfo = mTextComponent.textInfo;

            for (var i = 0; i < textInfo.linkCount; i++)
            {
                var linkInfo = textInfo.linkInfo[i];

                var isBeginRegion = false;

                var bottomLeft = Vector3.zero;
                var topLeft = Vector3.zero;
                var bottomRight = Vector3.zero;
                var topRight = Vector3.zero;

                var maxAscender = -Mathf.Infinity;
                var minDescender = Mathf.Infinity;

                Color32 linkColor = Color.cyan;

                // Iterate through each character of the link text
                for (var j = 0; j < linkInfo.linkTextLength; j++)
                {
                    var characterIndex = linkInfo.linkTextfirstCharacterIndex + j;
                    var currentCharInfo = textInfo.characterInfo[characterIndex];
                    var currentLine = currentCharInfo.lineNumber;

                    var isCharacterVisible = characterIndex > mTextComponent.maxVisibleCharacters ||
                                             currentCharInfo.lineNumber > mTextComponent.maxVisibleLines ||
                                             (mTextComponent.overflowMode == TextOverflowModes.Page &&
                                              currentCharInfo.pageNumber + 1 != mTextComponent.pageToDisplay)
                        ? false
                        : true;

                    // Track Max Ascender and Min Descender
                    maxAscender = Mathf.Max(maxAscender, currentCharInfo.ascender);
                    minDescender = Mathf.Min(minDescender, currentCharInfo.descender);

                    if (isBeginRegion == false && isCharacterVisible)
                    {
                        isBeginRegion = true;

                        bottomLeft = new Vector3(currentCharInfo.bottomLeft.x, currentCharInfo.descender, 0);
                        topLeft = new Vector3(currentCharInfo.bottomLeft.x, currentCharInfo.ascender, 0);

                        //Debug.Log("Start Word Region at [" + currentCharInfo.character + "]");

                        // If Link is one character
                        if (linkInfo.linkTextLength == 1)
                        {
                            isBeginRegion = false;

                            topLeft = _mTransform.TransformPoint(new Vector3(topLeft.x, maxAscender, 0));
                            bottomLeft = _mTransform.TransformPoint(new Vector3(bottomLeft.x, minDescender, 0));
                            bottomRight =
                                _mTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, minDescender, 0));
                            topRight = _mTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, maxAscender,
                                0));

                            // Draw Region
                            DrawRectangle(bottomLeft, topLeft, topRight, bottomRight, linkColor);

                            //Debug.Log("End Word Region at [" + currentCharInfo.character + "]");
                        }
                    }

                    // Last Character of Link
                    if (isBeginRegion && j == linkInfo.linkTextLength - 1)
                    {
                        isBeginRegion = false;

                        topLeft = _mTransform.TransformPoint(new Vector3(topLeft.x, maxAscender, 0));
                        bottomLeft = _mTransform.TransformPoint(new Vector3(bottomLeft.x, minDescender, 0));
                        bottomRight =
                            _mTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, minDescender, 0));
                        topRight = _mTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, maxAscender, 0));

                        // Draw Region
                        DrawRectangle(bottomLeft, topLeft, topRight, bottomRight, linkColor);

                        //Debug.Log("End Word Region at [" + currentCharInfo.character + "]");
                    }
                    // If Link is split on more than one line.
                    else if (isBeginRegion && currentLine != textInfo.characterInfo[characterIndex + 1].lineNumber)
                    {
                        isBeginRegion = false;

                        topLeft = _mTransform.TransformPoint(new Vector3(topLeft.x, maxAscender, 0));
                        bottomLeft = _mTransform.TransformPoint(new Vector3(bottomLeft.x, minDescender, 0));
                        bottomRight =
                            _mTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, minDescender, 0));
                        topRight = _mTransform.TransformPoint(new Vector3(currentCharInfo.topRight.x, maxAscender, 0));

                        // Draw Region
                        DrawRectangle(bottomLeft, topLeft, topRight, bottomRight, linkColor);

                        maxAscender = -Mathf.Infinity;
                        minDescender = Mathf.Infinity;
                        //Debug.Log("End Word Region at [" + currentCharInfo.character + "]");
                    }
                }

                //Debug.Log(wInfo.GetWord(m_TextMeshPro.textInfo.characterInfo));
            }
        }


        /// <summary>
        /// Draw Rectangles around each lines of the text.
        /// </summary>
        /// <param name="text"></param>
        private void DrawLineBounds()
        {
            var lineCount = _mTextInfo.lineCount;

            for (var i = 0; i < lineCount; i++)
            {
                var lineInfo = _mTextInfo.lineInfo[i];
                var firstCharacterInfo = _mTextInfo.characterInfo[lineInfo.firstCharacterIndex];
                var lastCharacterInfo = _mTextInfo.characterInfo[lineInfo.lastCharacterIndex];

                var isLineVisible = (lineInfo.characterCount == 1 && (firstCharacterInfo.character == 10 ||
                                                                      firstCharacterInfo.character == 11 ||
                                                                      firstCharacterInfo.character == 0x2028 ||
                                                                      firstCharacterInfo.character == 0x2029)) ||
                                    i > mTextComponent.maxVisibleLines ||
                                    (mTextComponent.overflowMode == TextOverflowModes.Page &&
                                     firstCharacterInfo.pageNumber + 1 != mTextComponent.pageToDisplay)
                    ? false
                    : true;

                if (!isLineVisible) continue;

                var lineBottomLeft = firstCharacterInfo.bottomLeft.x;
                var lineTopRight = lastCharacterInfo.topRight.x;

                var ascentline = lineInfo.ascender;
                var baseline = lineInfo.baseline;
                var descentline = lineInfo.descender;

                float dottedLineSize = 12;

                // Draw line extents
                DrawDottedRectangle(_mTransform.TransformPoint(lineInfo.lineExtents.min),
                    _mTransform.TransformPoint(lineInfo.lineExtents.max), Color.green, 4);

                // Draw Ascent line
                var ascentlineStart = _mTransform.TransformPoint(new Vector3(lineBottomLeft, ascentline, 0));
                var ascentlineEnd = _mTransform.TransformPoint(new Vector3(lineTopRight, ascentline, 0));

                Handles.color = Color.yellow;
                Handles.DrawDottedLine(ascentlineStart, ascentlineEnd, dottedLineSize);

                // Draw Base line
                var baseLineStart = _mTransform.TransformPoint(new Vector3(lineBottomLeft, baseline, 0));
                var baseLineEnd = _mTransform.TransformPoint(new Vector3(lineTopRight, baseline, 0));

                Handles.color = Color.yellow;
                Handles.DrawDottedLine(baseLineStart, baseLineEnd, dottedLineSize);

                // Draw Descent line
                var descentLineStart = _mTransform.TransformPoint(new Vector3(lineBottomLeft, descentline, 0));
                var descentLineEnd = _mTransform.TransformPoint(new Vector3(lineTopRight, descentline, 0));

                Handles.color = Color.yellow;
                Handles.DrawDottedLine(descentLineStart, descentLineEnd, dottedLineSize);

                // Draw text labels for metrics
                if (_mHandleSize < 1.0f)
                {
                    var style = new GUIStyle();
                    style.normal.textColor = new Color(0.8f, 0.8f, 0.8f, 1.0f);
                    style.fontSize = 12;
                    style.fixedWidth = 200;
                    style.fixedHeight = 20;
                    Vector3 labelPosition;

                    // Ascent Line
                    labelPosition = _mTransform.TransformPoint(new Vector3(lineBottomLeft, ascentline, 0));
                    style.padding = new RectOffset(0, 10, 0, 5);
                    style.alignment = TextAnchor.MiddleRight;
                    Handles.Label(labelPosition, "Ascent Line", style);

                    // Base Line
                    labelPosition = _mTransform.TransformPoint(new Vector3(lineBottomLeft, baseline, 0));
                    Handles.Label(labelPosition, "Base Line", style);

                    // Descent line
                    labelPosition = _mTransform.TransformPoint(new Vector3(lineBottomLeft, descentline, 0));
                    Handles.Label(labelPosition, "Descent Line", style);
                }
            }
        }


        /// <summary>
        /// Draw Rectangle around the bounds of the text object.
        /// </summary>
        private void DrawBounds()
        {
            var meshBounds = mTextComponent.bounds;

            // Get Bottom Left and Top Right position of each word
            var bottomLeft = mTextComponent.transform.position + meshBounds.min;
            var topRight = mTextComponent.transform.position + meshBounds.max;

            DrawRectangle(bottomLeft, topRight, new Color(1, 0.5f, 0));
        }


        private void DrawTextBounds()
        {
            var textBounds = mTextComponent.textBounds;

            var bottomLeft = mTextComponent.transform.position + (textBounds.center - textBounds.extents);
            var topRight = mTextComponent.transform.position + (textBounds.center + textBounds.extents);

            DrawRectangle(bottomLeft, topRight, new Color(0f, 0.5f, 0.5f));
        }


        // Draw Rectangles
        private void DrawRectangle(Vector3 bl, Vector3 tr, Color color)
        {
            Gizmos.color = color;

            Gizmos.DrawLine(new Vector3(bl.x, bl.y, 0), new Vector3(bl.x, tr.y, 0));
            Gizmos.DrawLine(new Vector3(bl.x, tr.y, 0), new Vector3(tr.x, tr.y, 0));
            Gizmos.DrawLine(new Vector3(tr.x, tr.y, 0), new Vector3(tr.x, bl.y, 0));
            Gizmos.DrawLine(new Vector3(tr.x, bl.y, 0), new Vector3(bl.x, bl.y, 0));
        }

        private void DrawDottedRectangle(Vector3 bottomLeft, Vector3 topRight, Color color, float size = 5.0f)
        {
            Handles.color = color;
            Handles.DrawDottedLine(bottomLeft, new Vector3(bottomLeft.x, topRight.y, bottomLeft.z), size);
            Handles.DrawDottedLine(new Vector3(bottomLeft.x, topRight.y, bottomLeft.z), topRight, size);
            Handles.DrawDottedLine(topRight, new Vector3(topRight.x, bottomLeft.y, bottomLeft.z), size);
            Handles.DrawDottedLine(new Vector3(topRight.x, bottomLeft.y, bottomLeft.z), bottomLeft, size);
        }

        private void DrawSolidRectangle(Vector3 bottomLeft, Vector3 topRight, Color color, float size = 5.0f)
        {
            Handles.color = color;
            var rect = new Rect(bottomLeft, topRight - bottomLeft);
            Handles.DrawSolidRectangleWithOutline(rect, color, Color.black);
        }

        private void DrawSquare(Vector3 position, float size, Color color)
        {
            Handles.color = color;
            var bottomLeft = new Vector3(position.x - size, position.y - size, position.z);
            var topLeft = new Vector3(position.x - size, position.y + size, position.z);
            var topRight = new Vector3(position.x + size, position.y + size, position.z);
            var bottomRight = new Vector3(position.x + size, position.y - size, position.z);

            Handles.DrawLine(bottomLeft, topLeft);
            Handles.DrawLine(topLeft, topRight);
            Handles.DrawLine(topRight, bottomRight);
            Handles.DrawLine(bottomRight, bottomLeft);
        }

        private void DrawCrosshair(Vector3 position, float size, Color color)
        {
            Handles.color = color;

            Handles.DrawLine(new Vector3(position.x - size, position.y, position.z),
                new Vector3(position.x + size, position.y, position.z));
            Handles.DrawLine(new Vector3(position.x, position.y - size, position.z),
                new Vector3(position.x, position.y + size, position.z));
        }


        // Draw Rectangles
        private void DrawRectangle(Vector3 bl, Vector3 tl, Vector3 tr, Vector3 br, Color color)
        {
            Gizmos.color = color;

            Gizmos.DrawLine(bl, tl);
            Gizmos.DrawLine(tl, tr);
            Gizmos.DrawLine(tr, br);
            Gizmos.DrawLine(br, bl);
        }


        // Draw Rectangles
        private void DrawDottedRectangle(Vector3 bl, Vector3 tl, Vector3 tr, Vector3 br, Color color)
        {
            var cam = Camera.current;
            var dotSpacing = (cam.WorldToScreenPoint(br).x - cam.WorldToScreenPoint(bl).x) / 75f;
            Handles.color = color;

            Handles.DrawDottedLine(bl, tl, dotSpacing);
            Handles.DrawDottedLine(tl, tr, dotSpacing);
            Handles.DrawDottedLine(tr, br, dotSpacing);
            Handles.DrawDottedLine(br, bl, dotSpacing);
        }
#endif
    }
}