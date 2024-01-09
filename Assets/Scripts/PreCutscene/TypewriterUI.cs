using System.Collections;
using TMPro;
using UnityEngine;

namespace PreCutscene
{
    public class TypewriterUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        private string _final;
        [SerializeField] private float timeBetween = 0.1f;

        public void Write()
        {
            StopAllCoroutines();
            _final = text.text;
            text.text = "";
            StartCoroutine(nameof(TypeWriter));
        }

        private IEnumerator TypeWriter()
        {
            foreach (var c in _final)
            {
                if (text.text.Length > 0) text.text = text.text.Substring(0, text.text.Length);
                text.text += c;
                yield return new WaitForSeconds(timeBetween);
            }
        }

        public void SetText(string text)
        {
            this.text.text = text;
        }

        public float GetTimeBetween()
        {
            return timeBetween;
        }
    }
}