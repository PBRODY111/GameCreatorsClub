using System.Collections;
using PreCutscene;
using Scene1.Safe;
using TMPro;
using UnityEngine;

namespace Scene1.Computer
{
    public class Ascal : MonoBehaviour
    {
        [SerializeField] private GameObject ascalUI;
        [SerializeField] private AudioSource ascalAudio;
        [SerializeField] private AudioSource ascalEffects;
        public AudioClip[] effects;
        [SerializeField] private TMP_Text displayText;
        [SerializeField] private GameObject letterInput;
        public string[] words1;
        public string[] words2;
        public string[] words3;
        [SerializeField] private string string1;
        [SerializeField] private string string2;
        [SerializeField] private string string3;
        [SerializeField] private string currentString;
        public string safe3Pwd;
        [SerializeField] private SafeDoor32 safeDoor32;
        public Color[] colors;
        public Color safe3Color;

        private readonly string[] _text =
        {
            "Hello there, you may call me Ascal.",
            "Can you help me remember?",
            "I can't remember anything after turning into this.",
            "I will give you a list of letters.",
            "You will have to enter the letters one by one in the same order.",
            "Are you ready?",
            "Here we go!",
            "in 3...",
            "2...",
            "1..."
        };

        private int _letterIndex;
        private TypewriterUI _typewriterUi;

        public void EnterAscal()
        {
            _letterIndex = 0;
            string1 = words1[Random.Range(0, words1.Length)];
            string2 = words2[Random.Range(0, words2.Length)];
            string3 = words3[Random.Range(0, words3.Length)];
            safe3Pwd = "" + string1[Random.Range(0, string1.Length)] + string2[Random.Range(0, string2.Length)] +
                       string3[Random.Range(0, string3.Length)];
            safeDoor32.code = safe3Pwd;
            safe3Color = colors[Random.Range(0, colors.Length)];
            safeDoor32.selectColor = safe3Color;

            string1 = Scramble(string1);
            string2 = Scramble(string2);
            string3 = Scramble(string3);

            ascalUI.SetActive(true);
            ascalAudio.Play();
            Debug.Log("Entered");
            Time.timeScale = 1f;
            _typewriterUi = transform.GetComponent<TypewriterUI>();
            StartCoroutine(AscalGame());
        }
        
        private static string Scramble(string s)
        {
            var array = s.ToCharArray();
            var rng = new System.Random();
            var n = array.Length;
            while (n > 1)
            {
                n--;
                var k = rng.Next(n + 1);
                (array[k], array[n]) = (array[n], array[k]);
            }

            return new string(array);
        }
        
        public void ExitAscal()
        {
            ascalUI.SetActive(false);
            ascalAudio.Stop();
        }

        public void GetLetter(string s)
        {
            s = s.ToLower();
            Debug.Log("" + currentString[_letterIndex]);
            AudioClip selectedClip;

            if ("" + s == "" + currentString[_letterIndex])
            {
                selectedClip = _letterIndex == currentString.Length - 1 ? effects[2] : effects[0];
            }
            else
            {
                selectedClip = effects[1];
                StartCoroutine(AscalIncorrect());
            }

            ascalEffects.clip = selectedClip;
            ascalEffects.Play();

            if (_letterIndex == currentString.Length - 1)
            {
                _letterIndex = 0;
                var routine = currentString switch
                {
                    _ when currentString == string1 => AscalGame1(),
                    _ when currentString == string2 => AscalGame2(),
                    _ => AscalWin()
                };
                StartCoroutine(routine);
            }
            else _letterIndex++;

            letterInput.GetComponent<TMP_InputField>().text = "";
        }

        private IEnumerator AscalGame()
        {
            letterInput.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            Debug.Log("ASCAL!");
            foreach (var line in _text)
            {
                _typewriterUi.SetText(line);
                _typewriterUi.Write();
                yield return new WaitForSeconds(_typewriterUi.GetTimeBetween() * line.Length + 1f);
            }

            yield return new WaitForSeconds(0.5f);
            foreach (var t in string1)
            {
                displayText.text = "" + t;
                yield return new WaitForSeconds(1.5f);
                displayText.text = "";
                yield return new WaitForSeconds(0.5f);
            }

            displayText.text = "Now your turn!";
            letterInput.SetActive(true);
            currentString = string1;
        }

        private IEnumerator AscalGame1()
        {
            Time.timeScale = 1f;
            Debug.Log("Level 1 done");
            letterInput.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _typewriterUi.SetText("Correct! The letters were: ");
            _typewriterUi.Write();
            yield return new WaitForSeconds(3f);
            foreach (var t in string1)
            {
                displayText.color = t == safe3Pwd[0] ? safe3Color : Color.black;

                displayText.text = "" + t;
                yield return new WaitForSeconds(1.5f);
                displayText.text = "";
                yield return new WaitForSeconds(0.5f);
            }

            displayText.color = Color.black;
            _typewriterUi.SetText("Round 2 starting...");
            _typewriterUi.Write();
            yield return new WaitForSeconds(3f);
            foreach (var t in string2)
            {
                displayText.text = "" + t;
                yield return new WaitForSeconds(2.5f);
                displayText.text = "";
                yield return new WaitForSeconds(0.5f);
            }

            displayText.text = "Now your turn!";
            letterInput.SetActive(true);
            currentString = string2;
        }

        private IEnumerator AscalGame2()
        {
            Time.timeScale = 1f;
            Debug.Log("Level 2 done");
            letterInput.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _typewriterUi.SetText("Correct! The letters were: ");
            _typewriterUi.Write();
            yield return new WaitForSeconds(3f);
            foreach (var t in string2)
            {
                displayText.color = t == safe3Pwd[1] ? safe3Color : Color.black;

                displayText.text = "" + t;
                yield return new WaitForSeconds(1.5f);
                displayText.text = "";
                yield return new WaitForSeconds(0.5f);
            }

            displayText.color = Color.black;
            _typewriterUi.SetText("Round 3 starting...");
            _typewriterUi.Write();
            yield return new WaitForSeconds(3f);
            foreach (var t in string3)
            {
                displayText.text = "" + t;
                yield return new WaitForSeconds(3.5f);
                displayText.text = "";
                yield return new WaitForSeconds(0.5f);
            }

            displayText.text = "Now your turn!";
            letterInput.SetActive(true);
            currentString = string3;
        }

        private IEnumerator AscalIncorrect()
        {
            Time.timeScale = 1f;
            letterInput.SetActive(false);
            _typewriterUi.SetText("Incorrect! Try again later...");
            yield return new WaitForSeconds(3f);
            ExitAscal();
        }

        private IEnumerator AscalWin()
        {
            Time.timeScale = 1f;
            Debug.Log("Level 3 done");
            letterInput.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _typewriterUi.SetText("Correct! The letters were: ");
            _typewriterUi.Write();
            yield return new WaitForSeconds(3f);
            foreach (var t in string3)
            {
                displayText.color = t == safe3Pwd[2] ? safe3Color : Color.black;

                displayText.text = "" + t;
                yield return new WaitForSeconds(1.5f);
                displayText.text = "";
                yield return new WaitForSeconds(0.5f);
            }

            displayText.color = Color.black;
            ascalEffects.clip = effects[3];
            ascalEffects.Play();
            _typewriterUi.SetText("GAME WIN!!!");
            yield return new WaitForSeconds(3f);
            ExitAscal();
        }
    }
}