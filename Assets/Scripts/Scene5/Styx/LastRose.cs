using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LastRose : MonoBehaviour
{
    [SerializeField] private AudioSource unlock;
    [SerializeField] private Animator lDoorAnim;
    [SerializeField] private Animator rDoorAnim;
    [SerializeField] private TextMeshProUGUI pointsText;
    private static readonly int IsOpen = Animator.StringToHash("isOpen");
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Zagreus")
        {
            lDoorAnim.SetBool(IsOpen, true);
            rDoorAnim.SetBool(IsOpen, true);
            unlock.Play();
            pointsText.text = "1/1";
            gameObject.SetActive(false);
        }
    }
}
