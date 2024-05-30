using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dpInhibitor : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private AudioSource switchAudio;
    [SerializeField] private AudioSource shutdownAudio;
    [SerializeField] private GameObject intText;
    [SerializeField] private GameObject light;
    [SerializeField] private GameObject booth;
    [SerializeField] private GameObject inhibitor;
    private bool canInteract = true;
    // Start is called before the first frame update
    void Start()
    {
        SaveData data = SaveSystem.LoadMinigame();
        if(data != null){
            if(!data.doublePong){
                booth.SetActive(true);
                inhibitor.SetActive(false);
            } else{
                booth.SetActive(false);
                light.SetActive(false);
                transform.rotation = Quaternion.Euler(0, 0, -90);
                canInteract = false;
            }
        }
    }

    private void OnMouseExit()
    {
        intText.SetActive(false);
    }

    private void OnMouseOver()
    {
        intText.SetActive(IsWithinReach() && canInteract);
        if (Input.GetKeyDown(KeyCode.E) && IsWithinReach() && canInteract)
        {
            switchAudio.Play();
            transform.rotation = Quaternion.Euler(0, 0, -90);
            light.SetActive(false);
            SaveSystem.SaveMinigame("doublePong");
            canInteract = false;
            shutdownAudio.Play();
        }
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
