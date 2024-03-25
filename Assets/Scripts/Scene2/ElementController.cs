using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ElementController : MonoBehaviour
{
    [SerializeField] private string [] elements;
    [SerializeField] private int [] elementNums;
    [SerializeField] private string [] elementAbrevs;
    [SerializeField] private TextMeshPro [] elementList;
    [SerializeField] private AudioSource buzzAudio;
    public Beaker beaker;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private GameObject noCorrosive;
    [SerializeField] private GameObject noCorrosive2;
    [SerializeField] private GameObject yesCorrosive;
    private int elementItem = 0;
    private string currElementAbrev = "";
    private int currElementNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        yesCorrosive.SetActive(false);
        foreach (TextMeshPro elementGO in elementList)
        {
            int randomIndex = Random.Range(0, elements.Length);
            elementGO.text = elements[randomIndex];
        }
    }

    public void ProceedButton(){
        StartCoroutine(ProceedFunc());
    }

    public IEnumerator ProceedFunc(){
        buzzAudio.Play();
        for (int i = 0; i < elements.Length; i++)
        {
            if (elementList[elementItem].text == elements[i])
            {
                currElementNum = elementNums[i];
                currElementAbrev = elementAbrevs[i];
                break; // Exit the loop once a match is found
            }
        }
        if(leftButton.GetComponentInChildren<TextMeshProUGUI>().text == currElementAbrev && IsWithinValue(currElementNum, int.Parse(rightButton.GetComponentInChildren<TextMeshProUGUI>().text), 1f)){
            if(elementItem<3){
                elementItem++;
            } else{
                beaker.ExitUI();
                noCorrosive.SetActive(false);
                noCorrosive2.SetActive(false);
                yesCorrosive.SetActive(true);
            }
        } else{
            Debug.Log(leftButton.GetComponentInChildren<TextMeshProUGUI>().text);
            Debug.Log(elementList[elementItem].text);
            beaker.StartCoroutine(beaker.CerKilled());
        }
        yield return new WaitForSeconds(1f);
        EventSystem.current.SetSelectedGameObject(null);
    }

    private static bool IsWithinValue(float value, float actual, float deviation) => actual >= value - deviation && actual <= value + deviation;
}
