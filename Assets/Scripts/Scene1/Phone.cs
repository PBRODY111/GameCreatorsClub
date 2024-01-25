using System.Collections;
using System.Collections.Generic;
using Scene1.Safe;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Scene1
{
    public class Phone : MonoBehaviour
    {
        [SerializeField] private GameObject phoneUI;
        [SerializeField] private GameObject intText;
        [SerializeField] private GameObject email3;
        [SerializeField] private GameObject email4;
        [SerializeField] private float reach;
        public string numb1;
        public string numb2;
        public string numb3;
        public string numb4;
        [SerializeField] private TextMeshProUGUI num1E;
        [SerializeField] private TextMeshProUGUI num2E;
        [SerializeField] private TextMeshProUGUI num3E;
        [SerializeField] private TextMeshProUGUI num4E;
        [SerializeField] private string numb5;
        [SerializeField] private AudioSource dial;
        [SerializeField] private AudioSource call;
        [SerializeField] private AudioSource numbAudio;
        public AudioClip[] numbers;
        [SerializeField] private SafeDoor2 safeCode;
        public string gotCode;
        private int _currentNumb;
        private string _entered = "";
        private readonly bool _isUnlocked = false;

        private void Start()
        {
            var existingNumbers = new HashSet<string> { numb5 };
            num1E.text = numb1 = GenerateUniqueNumber(existingNumbers);
            num2E.text = numb2 = GenerateUniqueNumber(existingNumbers);
            num3E.text = numb3 = GenerateUniqueNumber(existingNumbers);
            num4E.text = numb4 = GenerateUniqueNumber(existingNumbers);
        }
        
        private static string GenerateUniqueNumber(HashSet<string> existingNumbers)
        {
            string number;
            do
            {
                number = Random.Range(0, 10000000).ToString("D7");
            } while (existingNumbers.Contains(number));

            existingNumbers.Add(number);
            return number;
        }

        private void Update()
        {
            if (phoneUI.activeSelf)
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    phoneUI.SetActive(false);
                    PauseMenu.IsPaused = false;
                    Player.Player.Instance.LockCursor();
                    _entered = "";
                }
        }

        private void OnMouseExit()
        {
            intText.SetActive(false);
        }

        private void OnMouseOver()
        {
            if (!phoneUI.activeSelf && !_isUnlocked)
                intText.SetActive(IsWithinReach());
            if (Input.GetKeyDown(KeyCode.E) && !_isUnlocked && IsWithinReach())
            {
                _entered = "";
                PauseMenu.IsPaused = true;
                intText.SetActive(false);
                phoneUI.SetActive(true);
                Player.Player.Instance.UnlockCursor();
            }
        }

        public void AddNumb(Button button)
        {
            dial.Play();
            _entered += button.name;
            if (_entered.Length >= 7)
            {
                call.Play();
                StartCoroutine(WaitDial());
            }
        }

        private IEnumerator WaitDial()
        {
            phoneUI.SetActive(false);
            PauseMenu.IsPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            yield return new WaitForSeconds(8.0f);
            if (_entered == numb1)
            {
                numbAudio.clip = numbers[1];
                numbAudio.Play();
            }
            else if (_entered == numb2)
            {
                email3.SetActive(true);
                email4.SetActive(true);
                numbAudio.clip = numbers[2];
                numbAudio.Play();
            }
            else if (_entered == numb3)
            {
                gotCode = safeCode.code;
                for (var i = 0; i < gotCode.Length; i++)
                {
                    _currentNumb = int.Parse(gotCode[i].ToString());
                    numbAudio.clip = numbers[_currentNumb + 4];
                    numbAudio.Play();
                    yield return new WaitForSeconds(1.5f);
                }
            }
            else if (_entered == numb4)
            {
                numbAudio.clip = numbers[14];
                numbAudio.Play();
            }
            else if (_entered == numb5)
            {
                numbAudio.clip = numbers[3];
                numbAudio.Play();
            }
            else
            {
                numbAudio.clip = numbers[0];
                numbAudio.Play();
            }

            _entered = "";
        }

        private bool IsWithinReach()
        {
            return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
        }
    }
}