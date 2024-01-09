using TMPro;
using UnityEngine;

namespace Scene1.Safe
{
    public class SafeDoor31 : MonoBehaviour
    {
        [SerializeField] private GameObject intText3;
        [SerializeField] private float reach;
        [SerializeField] private AudioSource unlockAudio;
        public bool isUnlocked;
        private Animator _safeAnimator;

        private void Awake()
        {
            _safeAnimator = GetComponentInChildren<Animator>();
        }

        // Start is called before the first frame update
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }

        private void OnMouseExit()
        {
            intText3.SetActive(false);
        }

        private void OnMouseOver()
        {
            intText3.GetComponent<TMP_Text>().text = "LOCKPICK NEEDED TO INTERACT";
            if (isUnlocked == false)
                intText3.SetActive(IsWithinReach());
            else
                intText3.SetActive(false);

            if (Input.GetMouseButtonDown(1) && IsWithinReach() &&
                Player.Player.Instance.GetHeldItem().itemName == "Lock Pick")
            {
                _safeAnimator.SetBool("unlock", true);
                if (!isUnlocked) unlockAudio.Play();

                isUnlocked = true;
                intText3.SetActive(false);
            }
        }

        private bool IsWithinReach()
        {
            return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
        }
    }
}