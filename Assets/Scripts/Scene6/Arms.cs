using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scene6;

public class Arms : MonoBehaviour
{
    [SerializeField] private Engineer paradox;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Zagreus")
        {
            paradox.isActive = true;
        }
    }
}
