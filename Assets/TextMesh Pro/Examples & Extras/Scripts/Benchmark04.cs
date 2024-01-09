using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace TextMesh_Pro.Examples___Extras.Scripts
{
    public class Benchmark04 : MonoBehaviour
    {
        [FormerlySerializedAs("SpawnType")] public int spawnType;

        [FormerlySerializedAs("MinPointSize")] public int minPointSize = 12;
        [FormerlySerializedAs("MaxPointSize")] public int maxPointSize = 64;
        [FormerlySerializedAs("Steps")] public int steps = 4;

        private Transform _mTransform;
        //private TextMeshProFloatingText floatingText_Script;
        //public Material material;


        private void Start()
        {
            _mTransform = transform;

            float lineHeight = 0;
            var orthoSize = Camera.main.orthographicSize = Screen.height / 2;
            var ratio = (float)Screen.width / Screen.height;

            for (var i = minPointSize; i <= maxPointSize; i += steps)
                if (spawnType == 0)
                {
                    // TextMesh Pro Implementation
                    var go = new GameObject("Text - " + i + " Pts");

                    if (lineHeight > orthoSize * 2) return;

                    go.transform.position = _mTransform.position +
                                            new Vector3(ratio * -orthoSize * 0.975f, orthoSize * 0.975f - lineHeight,
                                                0);

                    var textMeshPro = go.AddComponent<TextMeshPro>();

                    //textMeshPro.fontSharedMaterial = material;
                    //textMeshPro.font = Resources.Load("Fonts & Materials/LiberationSans SDF", typeof(TextMeshProFont)) as TextMeshProFont;
                    //textMeshPro.anchor = AnchorPositions.Left;
                    textMeshPro.rectTransform.pivot = new Vector2(0, 0.5f);

                    textMeshPro.enableWordWrapping = false;
                    textMeshPro.extraPadding = true;
                    textMeshPro.isOrthographic = true;
                    textMeshPro.fontSize = i;

                    textMeshPro.text = i + " pts - Lorem ipsum dolor sit...";
                    textMeshPro.color = new Color32(255, 255, 255, 255);

                    lineHeight += i;
                }
            // TextMesh Implementation
            // Causes crashes since atlas needed exceeds 4096 X 4096
            /*
                    GameObject go = new GameObject("Arial " + i);

                    //if (lineHeight > orthoSize * 2 * 0.9f) return;

                    go.transform.position = m_Transform.position + new Vector3(ratio * -orthoSize * 0.975f, orthoSize * 0.975f - lineHeight, 1);

                    TextMesh textMesh = go.AddComponent<TextMesh>();
                    textMesh.font = Resources.Load("Fonts/ARIAL", typeof(Font)) as Font;
                    textMesh.renderer.sharedMaterial = textMesh.font.material;
                    textMesh.anchor = TextAnchor.MiddleLeft;
                    textMesh.fontSize = i * 10;

                    textMesh.color = new Color32(255, 255, 255, 255);
                    textMesh.text = i + " pts - Lorem ipsum dolor sit...";

                    lineHeight += i;
                    */
        }
    }
}