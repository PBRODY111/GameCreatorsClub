using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;

namespace TitleScene
{
    [ExecuteInEditMode]
    [AddComponentMenu("Image Effects/GlitchEffect")]
    [RequireComponent(typeof(Camera))]
    [RequireComponent(typeof(VideoPlayer))]
    public class VhsPostProcessEffect : MonoBehaviour
    {
        public Shader shader;
        [FormerlySerializedAs("VHSClip")] public VideoClip vhsClip;

        private float _yScanline;
        private float _xScanline;
        private Material _material;
        private VideoPlayer _player;
        private static readonly int VhsTex = Shader.PropertyToID("_VHSTex");
        private static readonly int YScanline = Shader.PropertyToID("_yScanline");
        private static readonly int XScanline = Shader.PropertyToID("_xScanline");

        private void Start()
        {
            _material = new Material(shader);
            _player = GetComponent<VideoPlayer>();
            _player.isLooping = true;
            _player.renderMode = VideoRenderMode.APIOnly;
            _player.audioOutputMode = VideoAudioOutputMode.None;
            _player.clip = vhsClip;
            _player.Play();
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            _material.SetTexture(VhsTex, _player.texture);

            _yScanline += Time.deltaTime * 0.01f;
            _xScanline -= Time.deltaTime * 0.1f;

            if (_yScanline >= 1)
            {
                _yScanline = Random.value;
            }

            if (_xScanline <= 0 || Random.value < 0.05)
            {
                _xScanline = Random.value;
            }

            _material.SetFloat(YScanline, _yScanline);
            _material.SetFloat(XScanline, _xScanline);
            Graphics.Blit(source, destination, _material);
        }

        protected void OnDisable()
        {
            if (_material)
            {
                DestroyImmediate(_material);
            }
        }
    }
}