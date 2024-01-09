using Player;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class Tutorial : MonoBehaviour
    {
        [FormerlySerializedAs("UI")]
        [SerializeField]
        private GameObject ui;

        private GameObject _page1;
        private GameObject _page2;
        private GameObject _page3;
        private GameObject _page4;

        public void Start()
        {
            ui.SetActive(false);
            _page1 = transform.GetChild(0).gameObject;
            _page2 = transform.GetChild(1).gameObject;
            _page3 = transform.GetChild(2).gameObject;
            _page4 = transform.GetChild(3).gameObject;
            gameObject.SetActive(true);
            _page1.SetActive(true);

            _page1.GetComponentInChildren<Button>().onClick.AddListener(GoPage2);
            _page2.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(GoPage1);
            _page2.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(GoPage3);
            _page3.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(GoPage2);
            _page3.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(GoPage4);
            _page4.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(GoPage3);
            _page4.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(Hide);
            Player.Player.Instance.mainCamera.GetComponent<PlayerCam>().enabled = false;
            Player.Player.Instance.GetComponent<PlayerMovement>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
        }

        public void GoPage1()
        {
            _page1.SetActive(true);
            _page2.SetActive(false);
            _page3.SetActive(false);
            _page4.SetActive(false);
        }

        public void GoPage2()
        {
            _page1.SetActive(false);
            _page2.SetActive(true);
            _page3.SetActive(false);
            _page4.SetActive(false);
        }

        public void GoPage3()
        {
            _page1.SetActive(false);
            _page2.SetActive(false);
            _page3.SetActive(true);
            _page4.SetActive(false);
        }

        public void GoPage4()
        {
            _page1.SetActive(false);
            _page2.SetActive(false);
            _page3.SetActive(false);
            _page4.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
            Player.Player.Instance.mainCamera.GetComponent<PlayerCam>().enabled = true;
            Player.Player.Instance.GetComponent<PlayerMovement>().enabled = true;
            ui.SetActive(true);
        }
    }
}