using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElementController : MonoBehaviour
{
    [SerializeField] private string [] elements;
    [SerializeField] private TextMeshPro [] elementList;
    // Start is called before the first frame update
    void Start()
    {
        foreach (TextMeshPro elementGO in elementList)
        {
            int randomIndex = Random.Range(0, elements.Length);
            elementGO.text = elements[randomIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
