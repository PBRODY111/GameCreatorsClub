using UnityEngine;
using TMPro;

public class SafeDoor4 : MonoBehaviour
{
    [SerializeField] private GameObject intText3;
    [SerializeField] private float reach;
    [SerializeField] private AudioSource unlockAudio;
    private Animator _safeAnimator;

    public bool isUnlocked;

    // Start is called before the first frame update
    private void Awake()
    {
        _safeAnimator = GetComponentInChildren<Animator>();
    }

    private void OnMouseOver()
    {
        intText3.GetComponent<TMP_Text>().text = "SCREWDRIVER NEEDED TO INTERACT";
        if (isUnlocked == false)
        {
            intText3.SetActive(IsWithinReach());
        }
        else
        {
            intText3.SetActive(false);
        }

        if (Input.GetMouseButtonDown(1) && IsWithinReach())
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

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position) <= reach;
    }
}