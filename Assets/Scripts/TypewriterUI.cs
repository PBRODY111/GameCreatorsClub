using System.Collections;
using UnityEngine;
using TMPro;

public class typewriterUI : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
	private string final;
    [SerializeField] private float timeBetween = 0.1f; 

    public void Write()
    {
        StopAllCoroutines();
        final = text.text;
        text.text = "";
        StartCoroutine("TypeWriter");
    }

	IEnumerator TypeWriter()
    {
		foreach (char c in final)
		{
			if (text.text.Length > 0) text.text = text.text.Substring(0, text.text.Length);
			text.text += c;
			yield return new WaitForSeconds(timeBetween);
		}
	}

    public void setText(string text)
    {
        this.text.text = text;
    }

    public float getTimeBetween()
    {
        return timeBetween;
    }
}