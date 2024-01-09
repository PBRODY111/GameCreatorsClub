using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace TextMesh_Pro.Examples___Extras.Scripts
{
    public class ShaderPropAnimator : MonoBehaviour
    {
        [FormerlySerializedAs("GlowCurve")] public AnimationCurve glowCurve;

        [FormerlySerializedAs("m_frame")] public float mFrame;
        private Material _mMaterial;
        private Renderer _mRenderer;

        private void Awake()
        {
            // Cache a reference to object's renderer
            _mRenderer = GetComponent<Renderer>();

            // Cache a reference to object's material and create an instance by doing so.
            _mMaterial = _mRenderer.material;
        }

        private void Start()
        {
            StartCoroutine(AnimateProperties());
        }

        private IEnumerator AnimateProperties()
        {
            //float lightAngle;
            float glowPower;
            mFrame = Random.Range(0f, 1f);

            while (true)
            {
                //lightAngle = (m_Material.GetFloat(ShaderPropertyIDs.ID_LightAngle) + Time.deltaTime) % 6.2831853f;
                //m_Material.SetFloat(ShaderPropertyIDs.ID_LightAngle, lightAngle);

                glowPower = glowCurve.Evaluate(mFrame);
                _mMaterial.SetFloat(ShaderUtilities.ID_GlowPower, glowPower);

                mFrame += Time.deltaTime * Random.Range(0.2f, 0.3f);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}