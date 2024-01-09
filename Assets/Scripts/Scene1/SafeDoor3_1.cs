using UnityEngine;
using TMPro;

public class SafeDoor31 : MonoBehaviour
{
    [SerializeField] private GameObject intText3;
    [SerializeField] private float reach;
    [SerializeField] private AudioSource unlockAudio;
    private Animator _safeAnimator;
    public bool isUnlocked;

    // Start is called before the first frame update
    private void Start()
    {
    }

    private void Awake()
    {
        _safeAnimator = GetComponentInChildren<Animator>();
    }

    private void OnMouseOver()
    {
        intText3.GetComponent<TMP_Text>().text = "LOCKPICK NEEDED TO INTERACT";
        if (isUnlocked == false)
        {
            intText3.SetActive(IsWithinReach());
        }
        else
        {
            intText3.SetActive(false);
        }

        if (Input.GetMouseButtonDown(1) && IsWithinReach() && Player.Instance.GetHeldItem().itemName == "Lock Pick")
        {
            _safeAnimator.SetBool("unlock", true);
            if (!isUnlocked)
            {
                unlockAudio.Play();
            }

            isUnlocked = true;
            intText3.SetActive(false);
        }
    }

    private void OnMouseExit()
    {
        intText3.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position) <= reach;
    }
}