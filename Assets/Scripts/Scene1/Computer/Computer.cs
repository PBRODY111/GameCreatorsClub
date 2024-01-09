using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Scene1.Computer
{
    public class Computer : MonoBehaviour
    {
        [SerializeField] private GameObject intText;
        [SerializeField] private TextMeshPro pwdText;
        [SerializeField] private GameObject computerUI;
        [SerializeField] private GameObject loginPage;
        [SerializeField] private GameObject homePage;
        [SerializeField] private GameObject emails;
        [SerializeField] private GameObject email1;
        [SerializeField] private GameObject email2;
        [SerializeField] private GameObject email3;
        [SerializeField] private GameObject email4;
        [SerializeField] private GameObject notes;
        [SerializeField] private GameObject trash;
        public InputField pwdField;
        [SerializeField] private float reach;
        public string password = "";

        private readonly string[] _alphabet = new string[26]
        {
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u",
            "v",
            "w", "x", "y", "z"
        };

        private string _input;
        private readonly bool _isUnlocked = false;

        // Start is called before the first frame update
        private void Start()
        {
            for (var i = 0; i < 3; i++) password += _alphabet[Random.Range(0, _alphabet.Length)];

            for (var i = 0; i < 3; i++) password += Random.Range(0, 10);

            //pwdText = pwdText.GetComponent<TextMeshProUGUI>();
            pwdText.text = password;
        }

        // Update is called once per frame
        private void Update()
        {
            if (computerUI.activeSelf)
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    computerUI.SetActive(false);
                    Time.timeScale = 1f;
                    PauseMenu.IsPaused = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
        }

        private void OnMouseExit()
        {
            intText.SetActive(false);
        }

        private void OnMouseOver()
        {
            if (!computerUI.activeSelf && !_isUnlocked)
                intText.SetActive(IsWithinReach());
            if (Input.GetKeyDown(KeyCode.E) && !_isUnlocked && IsWithinReach())
            {
                intText.SetActive(false);
                computerUI.SetActive(true);
                Time.timeScale = 0f;
                PauseMenu.IsPaused = true;
                Cursor.lockState = computerUI.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
            }
        }

        public void ShutDown()
        {
            computerUI.SetActive(false);
            Time.timeScale = 1f;
            PauseMenu.IsPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void LogIn(string s)
        {
            _input = s;
            if (_input == password || Player.Player.Instance.EpicModeEnabled())
            {
                loginPage.SetActive(false);
                homePage.SetActive(true);
                //pwdField.Select();
                //pwdField.text = "";
            }
        }

        public void LogOut()
        {
            loginPage.SetActive(true);
            homePage.SetActive(false);
            emails.SetActive(false);
            notes.SetActive(false);
            trash.SetActive(false);
        }

        public void Desktop()
        {
            emails.SetActive(false);
            notes.SetActive(false);
            trash.SetActive(false);
            homePage.SetActive(true);
        }

        // Emails

        public void EmailApp()
        {
            emails.SetActive(true);
            homePage.SetActive(false);
        }

        public void Email1()
        {
            //GetComponent<Image>().color = Color.red;
            email1.SetActive(true);
        }

        public void ReturnEmail1()
        {
            email1.SetActive(false);
        }

        public void Email2()
        {
            //GetComponent<Image>().color = Color.red;
            email2.SetActive(true);
        }

        public void ReturnEmail2()
        {
            email2.SetActive(false);
        }

        public void Email3()
        {
            //GetComponent<Image>().color = Color.red;
            email3.SetActive(true);
        }

        public void ReturnEmail3()
        {
            email3.SetActive(false);
        }

        public void Email4()
        {
            //GetComponent<Image>().color = Color.red;
            email4.SetActive(true);
        }

        public void ReturnEmail4()
        {
            email4.SetActive(false);
        }

        public void NoteApp()
        {
            notes.SetActive(true);
            homePage.SetActive(false);
        }

        public void TrashApp()
        {
            trash.SetActive(true);
            homePage.SetActive(false);
        }

        private bool IsWithinReach()
        {
            return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
        }

        private bool IsWithinValue(float value, float actual, float deviation)
        {
            return actual >= value - deviation && actual <= value + deviation;
        }
    }
}